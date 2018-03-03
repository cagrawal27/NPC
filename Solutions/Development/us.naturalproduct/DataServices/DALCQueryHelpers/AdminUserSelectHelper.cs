//TODO: Delete Later

//using System;
//using System.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using MN.Enterprise.Base;
//using MN.Enterprise.Data;
//using us.naturalproduct.DataTransferObjects;

//namespace us.naturalproduct.DALCQueryHelpers
//{
//    public class AdminUserSelectHelper : DALCHelper
//    {
//        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
//        {
//            User userDto = criteria as User;

//            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminGetUser");

//            cw.AddInParameter("UserId", DbType.Int32, userDto.UserId);

//            return cw;
//        }

//        public override DataTransferObject ConvertResultsDto(DbCommandWrapper cw, IDataReader reader)
//        {
//            User userDto = null;

//            if (reader.Read())
//            {
//                userDto = new User();

//                userDto.UserId = (Int32) reader["UserId"];

//                userDto.EmailAddress = (string) reader["Email"];

//                userDto.FirstName = (string) reader["FirstName"];

//                userDto.LastName = (string) reader["LastName"];

//                userDto.MiddleInitial = (string) reader["MiddleInitial"];

//                userDto.AccountStatus = (Int32) reader["AccountStatus"];

//                userDto.AccountType = (Int32) reader["AccountTypeId"];

//                userDto.IsActive = (bool) reader["Active"];
//            }

//            return userDto;
//        }
//    }
//}