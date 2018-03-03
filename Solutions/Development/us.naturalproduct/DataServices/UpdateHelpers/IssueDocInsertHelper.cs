using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class IssueDocInsertHelper : BaseDataAccessHelper
    {
        public IssueDocInsertHelper()
        {
        }

        private DbCommand dbCommand;
        private string sqlCommand = "dbo.spInsertIssueDoc";


        public void InitCommand(Database db, Int32 volumeIssueId, IssueDocument issueDoc)
        {
            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "VolumeIssueId", DbType.Int32, volumeIssueId);
            db.AddInParameter(dbCommand, "IssueDocTypeId", DbType.Int32, issueDoc.IssueDocTypeId);
            db.AddInParameter(dbCommand, "Data", DbType.Binary, issueDoc.Data);
            db.AddInParameter(dbCommand, "FileName", DbType.String, issueDoc.FileName);
            db.AddInParameter(dbCommand, "FileSizeKB", DbType.Int32, issueDoc.CalculateFileSizeInKB());
            db.AddInParameter(dbCommand, "Comments", DbType.String, issueDoc.Comments);
            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(issueDoc.IsActive));
            db.AddInParameter(dbCommand, "CreationUserId", DbType.Int32, issueDoc.CreationUserId);
        }

        public void Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}