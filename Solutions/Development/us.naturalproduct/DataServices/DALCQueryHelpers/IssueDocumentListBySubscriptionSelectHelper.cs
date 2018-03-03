using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class IssueDocumentListBySubscriptionSelectHelper: DALCHelper<IssueDocument>
    {
        public override DbCommandWrapper InitializeCommand(Microsoft.Practices.EnterpriseLibrary.Data.Database db, MN.Enterprise.Base.DataTransferObject criteria)
        {
            Subscription subscriptionDto = criteria as Subscription;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetIssueDocumentsBySubscription");

            cw.AddInParameter("UserId", DbType.Int32, subscriptionDto.UserId);

            cw.AddInParameter("VolumeIssueId", DbType.Int32, subscriptionDto.VolumeIssueId);

            return cw;              
        }

        public override List<IssueDocument> ConvertResultsList(DbCommandWrapper cw, System.Data.IDataReader reader)
        {
            if (reader == null)
                return null;

            List<IssueDocument> issueDocuments = new List<IssueDocument>();

            while (reader.Read())
            {
                //Init new IssueDocument object for each IssueDocument record
                IssueDocument issueDocumentDto = new IssueDocument();

                //Populate IssueDocument object from record
                issueDocumentDto.IssueDocTypeDescription = (string)reader["IssueDocTypeDescription"];

                issueDocumentDto.DocId = (Int32)reader["DocId"];

                issueDocumentDto.FileSizeKB = (Int32)reader["FileSizeKB"];

                //Add to list array
                issueDocuments.Add(issueDocumentDto);
            }

            if (issueDocuments.Count == 0)
                return null;

            return issueDocuments;
            
        }

    }
}
