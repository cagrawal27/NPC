using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class IssueUpdateHelper : BaseDataAccessHelper
    {
        public IssueUpdateHelper()
        {
        }

        private DbCommand dbCommand;

        public void InitCommand(Database db, Issue issue)
        {
            string sqlCommand = "dbo.spUpdateIssue";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "VolumeIssueId", DbType.Int32, issue.VolumeIssueId);

            db.AddInParameter(dbCommand, "IssueName", DbType.String, issue.IssueName);

            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(issue.IsActive));

            db.AddInParameter(dbCommand, "UpdateUserId", DbType.Int32, issue.UpdateUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="txn"></param>
        /// <returns>A Int32 variable containing the VolumeIssueId</returns>
        public void Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);
        }
    }
}