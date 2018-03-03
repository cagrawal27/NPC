using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.QueryHelpers;
using us.naturalproduct.UpdateHelpers;

namespace us.naturalproduct.DataAccessObjects
{
    public class IssueDao
    {
        public static IssueArchive GetActiveIssues(Int32 VolumeId)
        {
            IssueArchive issArch = new IssueArchive();

            try
            {
                DataSet issueSet = IssueQueryHelper.GetActiveIssues(VolumeId);

                if (issueSet.Tables.Count == 2)
                {
                    List<Issue> issueList = new List<Issue>();

                    Volume vol;

                    Issue issue;

                    //Get the volume Name
                    if (issueSet.Tables[0].Rows.Count > 0)
                    {
                        vol = new Volume();

                        vol.VolumeName = (string) issueSet.Tables[0].Rows[0]["VolumeName"];

                        vol.VolumeYear = (string) issueSet.Tables[0].Rows[0]["VolumeYear"];

                        issArch.VolumeDto = vol;
                    }

                    //Get the issues
                    if (issueSet.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in issueSet.Tables[1].Rows)
                        {
                            issue = new Issue();

                            issue.VolumeIssueId = (Int32) dr["VolumeIssueId"];

                            issue.IssueName = (string) dr["IssueName"];

                            issueList.Add(issue);
                        }
                        issArch.IssueList = issueList;
                    }
                }

                return issArch;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the issue list.", sqlEx);
            }
        }

        public static Issue AdminGetIssue(Int32 VolumeIssueId)
        {
            Issue issue = new Issue();

            try
            {
                DataSet issueSet = IssueQueryHelper.AdminGetIssue(VolumeIssueId);

                if (issueSet.Tables.Count == 2)
                {
                    //Get the issue details
                    if (issueSet.Tables[0].Rows.Count > 0)
                    {
                        DataRow issRow = issueSet.Tables[0].Rows[0];

                        issue.IssueId = (Int32) issRow["IssueId"];

                        issue.IssueName = (string) issRow["IssueName"];

                        issue.IsActive = (bool) issRow["Active"];
                    }

                    //Get the documents
                    if (issueSet.Tables[1].Rows.Count > 0)
                    {
                        IssueDocument issDoc;

                        issue.Documents = new List<IssueDocument>();

                        foreach (DataRow docRow in issueSet.Tables[1].Rows)
                        {
                            issDoc = new IssueDocument();

                            issDoc.DocId = (Int32) docRow["DocId"];

                            issDoc.FullFileName = (string) docRow["FileName"];

                            issDoc.IssueDocTypeDescription = (string) docRow["IssueDocTypeDescription"];

                            issDoc.IssueDocTypeId = (Int32) docRow["IssueDocTypeId"];

                            issDoc.IsActive = (bool) docRow["Active"];

                            issDoc.IsNew = false;

                            issue.Documents.Add(issDoc);
                        }
                    }
                }

                return issue;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the issue details.", sqlEx);
            }
        }

        public static DataTable AdminGetIssues(Int32 VolumeId)
        {
            try
            {
                return IssueQueryHelper.AdminGetIssues(VolumeId);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the issue details.", sqlEx);
            }
        }

        public static ActionStatus AddIssue(Issue issueDto)
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            ActionStatus status = new ActionStatus();

            Int32 volumeIssueId;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();

                DbTransaction txn = null;

                try
                {
                    //Begin the transaction
                    txn = connection.BeginTransaction();

                    IssueInsertHelper issueHlpr = new IssueInsertHelper();

                    issueHlpr.InitCommand(db, issueDto);

                    volumeIssueId = issueHlpr.Execute(db, txn);

                    foreach (IssueDocument doc in issueDto.Documents)
                    {
                        //Load the data in a stream
                        doc.LoadData();

                        IssueDocInsertHelper issueDocHlpr = new IssueDocInsertHelper();

                        issueDocHlpr.InitCommand(db, volumeIssueId, doc);

                        issueDocHlpr.Execute(db, txn);
                    }

                    status.IsSuccessful = true;

                    // Commit the transaction.
                    txn.Commit();
                }
                catch (SqlException sqlEx)
                {
                    // Roll back the transaction. 
                    txn.Rollback();

                    Console.WriteLine(sqlEx.ToString());

                    throw new DataException("An exception occured adding an issue into the database.", sqlEx);
                }
                catch (IOException ioEx)
                {
                    // Roll back the transaction. 
                    txn.Rollback();

                    Console.WriteLine(ioEx.ToString());

                    throw new DataException("An exception occured trying to read document data.", ioEx);
                }
                finally
                {
                    connection.Close();
                }
            }

            return status;
        }

        public static ActionStatus UpdateIssue(Issue issueDto)
        {
            Database db = DatabaseFactory.CreateDatabase();

            ActionStatus status = new ActionStatus();

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();

                DbTransaction txn = null;

                try
                {
                    //Begin the transaction
                    txn = connection.BeginTransaction();

                    IssueUpdateHelper issueHlpr = new IssueUpdateHelper();

                    issueHlpr.InitCommand(db, issueDto);

                    issueHlpr.Execute(db, txn);

                    foreach (IssueDocument doc in issueDto.Documents)
                    {
                        if (doc.IsNew)
                        {
                            //Load the data in a stream
                            doc.LoadData();

                            IssueDocInsertHelper issueDocHlpr = new IssueDocInsertHelper();

                            issueDocHlpr.InitCommand(db, issueDto.VolumeIssueId, doc);

                            issueDocHlpr.Execute(db, txn);
                        }

                        if (doc.IsDeleted)
                        {
                            IssueDocDeleteHelper issueDocDelHlpr = new IssueDocDeleteHelper();

                            issueDocDelHlpr.InitCommand(db, issueDto.VolumeIssueId, doc);

                            issueDocDelHlpr.Execute(db, txn);
                        }
                    }

                    status.IsSuccessful = true;

                    // Commit the transaction.
                    txn.Commit();
                }
                catch (SqlException sqlEx)
                {
                    // Roll back the transaction. 
                    txn.Rollback();

                    Console.WriteLine(sqlEx.ToString());

                    throw new DataException("An exception occured updating an issue in the database.", sqlEx);
                }
                catch (IOException ioEx)
                {
                    // Roll back the transaction. 
                    txn.Rollback();

                    Console.WriteLine(ioEx.ToString());

                    throw new DataException("An exception occured trying to read document data.", ioEx);
                }
                finally
                {
                    connection.Close();
                }
            }

            return status;
        }
    }
}