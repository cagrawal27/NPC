using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class IssueInsertHelper : BaseDataAccessHelper
    {
        public IssueInsertHelper()
        {
        }

        private DbCommand dbCommand;

        public void InitCommand(Database db, Issue issue)
        {
            string sqlCommand = "dbo.spInsertIssue";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "VolumeIssueId", DbType.Int32, 4);

            db.AddInParameter(dbCommand, "VolumeId", DbType.Int32, issue.ParentVolume.VolumeId);
            db.AddInParameter(dbCommand, "IssueName", DbType.String, issue.IssueName);
            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(issue.IsActive));
            db.AddInParameter(dbCommand, "CreationUserId", DbType.Int32, issue.CreationUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="txn"></param>
        /// <returns>A Int32 variable containing the VolumeIssueId</returns>
        public Int32 Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);

            return (Int32) dbCommand.Parameters[0].Value;
        }
    }
}