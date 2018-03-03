using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public abstract class UserSelectHelper : DALCHelper
    {
        public override DataTransferObject ConvertResultsDto(DbCommandWrapper cw, IDataReader reader)
        {
            User userDto = null;

            if (reader.Read())
            {
                userDto = new User();

                userDto.UserId = (Int32) reader["UserId"];

                userDto.EmailAddress = (string) reader["Email"];

                userDto.PasswordHash = (string) reader["PasswordHash"];

                userDto.PasswordSalt = (string) reader["PasswordSalt"];

                userDto.FirstName = (string) reader["FirstName"];

                userDto.LastName = (string) reader["LastName"];

                userDto.MiddleInitial = (string) reader["MiddleInitial"];

                userDto.SecretQuestion1Id = (Int32) reader["SecretQuestion1Id"];

                userDto.SecretQuestion2Id = (Int32) reader["SecretQuestion2Id"];

                userDto.SecretAnswer1Hash = (string) reader["SecretAnswer1Hash"];

                userDto.SecretAnswer2Hash = (string) reader["SecretAnswer2Hash"];

                userDto.AccountStatus = (Int32) reader["AccountStatus"];

                userDto.AccountType = (Int32) reader["AccountTypeId"];

                userDto.IsActive = (bool) reader["Active"];

                userDto.CreationUserId = Convert.ToInt32(reader["CreationUserId"]);

                userDto.CreationDateTime = Convert.ToDateTime(reader["CreationDateTime"]);

                userDto.UpdateUserId = Convert.ToInt32(reader["UpdateUserId"]);

                userDto.UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"]);
            }

            return userDto;
        }
    }
}