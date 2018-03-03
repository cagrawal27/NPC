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
    public class ArticleDao
    {
        public static ActionStatus AddArticle(Article articleDto)
        {
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            SqlDatabase db = new SqlDatabase(Config.ConnString);

            ActionStatus status = new ActionStatus();

            Int32 articleId;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();

                DbTransaction txn = null;

                try
                {
                    //Begin the transaction
                    txn = connection.BeginTransaction();

                    ArticleInsertHelper artHlpr = new ArticleInsertHelper();

                    artHlpr.InitCommand(db, articleDto);

                    articleId = artHlpr.Execute(db, txn);

                    foreach (ArticleDocument doc in articleDto.Documents)
                    {
                        //Load the data in a stream
                        doc.LoadData();

                        ArticleDocInsertHelper artDocHlpr = new ArticleDocInsertHelper();

                        artDocHlpr.InitCommand(db, articleId, doc);

                        artDocHlpr.Execute(db, txn);
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

                    throw new DataException("An exception occured adding an article into the database.", sqlEx);
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

        public static Article AdminGetArticle(Int32 ArticleId)
        {
            Article art = new Article();

            try
            {
                DataSet articleSet = ArticleQueryHelper.AdminGetArticle(ArticleId);

                if (articleSet.Tables.Count == 2)
                {
                    //Get the issue details
                    if (articleSet.Tables[0].Rows.Count > 0)
                    {
                        DataRow artRow = articleSet.Tables[0].Rows[0];

                        art.ArticleId = (Int32) artRow["ArticleId"];

                        art.Title = (string) artRow["Title"];

                        art.Authors = (string) artRow["Authors"];

                        art.Keywords = (string) artRow["Keywords"];

                        art.PageNumber = (Int32) artRow["PageNumber"];

                        art.IsActive = (bool) artRow["Active"];
                    }

                    //Get the documents
                    if (articleSet.Tables[1].Rows.Count > 0)
                    {
                        ArticleDocument artDoc;

                        art.Documents = new List<ArticleDocument>();

                        foreach (DataRow docRow in articleSet.Tables[1].Rows)
                        {
                            artDoc = new ArticleDocument();

                            artDoc.DocId = (Int32) docRow["DocId"];

                            artDoc.FullFileName = (string) docRow["FileName"];

                            artDoc.ArtDocTypeDescription = (string) docRow["ArtDocTypeDescription"];

                            artDoc.ArtDocTypeId = (Int32) docRow["ArtDocTypeId"];

                            artDoc.IsActive = (bool) docRow["Active"];

                            artDoc.IsNew = false;

                            art.Documents.Add(artDoc);
                        }
                    }
                }

                return art;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the article details.", sqlEx);
            }
        }

        public static DataTable AdminGetArticles(Int32 VolumeIssueId)
        {
            try
            {
                return ArticleQueryHelper.AdminGetArticles(VolumeIssueId);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting all articles.", sqlEx);
            }
        }

        public static ActionStatus UpdateArticle(Article artDto)
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

                    ArticleUpdateHelper artHlpr = new ArticleUpdateHelper();

                    artHlpr.InitCommand(db, artDto);

                    artHlpr.Execute(db, txn);

                    foreach (ArticleDocument doc in artDto.Documents)
                    {
                        if (doc.IsNew)
                        {
                            //Load the data in a stream
                            doc.LoadData();

                            ArticleDocInsertHelper artDocHlpr = new ArticleDocInsertHelper();

                            artDocHlpr.InitCommand(db, artDto.ArticleId, doc);

                            artDocHlpr.Execute(db, txn);
                        }

                        if (doc.IsDeleted)
                        {
                            ArticleDocDeleteHelper artDocDelHlpr = new ArticleDocDeleteHelper();

                            artDocDelHlpr.InitCommand(db, artDto.ArticleId, doc);

                            artDocDelHlpr.Execute(db, txn);
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

                    throw new DataException("An exception occured updating an article in the database.", sqlEx);
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