using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.UpdateHelpers
{
    public class AddressInsertHelper : BaseDataAccessHelper
    {
        private DbCommand dbCommand;

        public AddressInsertHelper()
        {
        }

        public void InitCommand(Database db, Registration regDto, Int32 UserId)
        {
            string sqlCommand = "dbo.spInsertUserAddress";

            dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddParameter(dbCommand, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, null,
                            DataRowVersion.Default, null);

            db.AddInParameter(dbCommand, "UserId", DbType.Int32, UserId);
            db.AddInParameter(dbCommand, "AddressTypeId", DbType.String, regDto.AddressTypeId);
            db.AddInParameter(dbCommand, "Line1", DbType.String, regDto.AddressLine1);
            db.AddInParameter(dbCommand, "Line2", DbType.String, regDto.AddressLine2);
            db.AddInParameter(dbCommand, "City", DbType.String, regDto.City);
            db.AddInParameter(dbCommand, "StateProvince", DbType.String, regDto.State);
            db.AddInParameter(dbCommand, "CountryId", DbType.Int32, regDto.CountryId);
            db.AddInParameter(dbCommand, "Phone", DbType.String, regDto.Phone);
            db.AddInParameter(dbCommand, "Fax", DbType.String, regDto.Fax);
            db.AddInParameter(dbCommand, "Active", DbType.Int32, ConvBoolToInt32(regDto.IsActive));
            db.AddInParameter(dbCommand, "CreationUserId", DbType.Int16, regDto.CreationUserId);
        }

        public Int32 Execute(Database db, DbTransaction txn)
        {
            db.ExecuteNonQuery(dbCommand, txn);

            return (Int32) dbCommand.Parameters[0].Value;
        }
    }
}