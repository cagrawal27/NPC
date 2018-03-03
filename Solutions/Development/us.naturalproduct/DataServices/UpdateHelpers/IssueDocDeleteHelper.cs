using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class IssueDocDeleteHelper : BaseDataAccessHelper
    {
        public IssueDocDeleteHelper()
        {
        }

        private DbCommand dbCommand;
        private string sqlCommand = "dbo.spDeleteIssueDoc";

        public void InitCommand(Database db, Int32 volumeIssueId, IssueDocument issueDoc)
        {
            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "VolumeIssueId", DbType.Int32, volumeIssueId);
            db.AddInParameter(dbCommand, "IssueDocTypeId", DbType.Int32, issueDoc.IssueDocTypeId);
            db.AddInParameter(dbCommand, "DocId", DbType.Int32, issueDoc.DocId);
        }

        public void Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}