using System;
using System.Data;
using MN.Enterprise.Data;
using us.naturalproduct.DALCDeleteHelpers;
using us.naturalproduct.DALCHelpers;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DALCUpdateHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class UserDalc : CommonDALC
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserDalc() : base()
        {
        }

        /// <summary>
        /// Constructor with a transaction.
        /// </summary>
        /// <param name="transaction"></param>
        public UserDalc(DALCTransaction transaction)
            : base(transaction)
        {
        }

        #endregion
        
        public bool Exists(User inUserDto)
        {
            IDataReader rdr = ExecuteQueryReader(new UserExistsHelper(), inUserDto);

            bool found = false;

            if (rdr != null)
            {
                if (rdr.Read())
                {
                    Int32 exists = (Int32)rdr["Exists"];

                    found = (exists > 0);
                }
            }

            return found;            
        }

        public User GetUser(User inUserDto)
        {
            if (null == inUserDto)
                throw new ArgumentNullException("inUserDto");

            User outUserDto = null;

            if (inUserDto.EmailAddress != null && inUserDto.EmailAddress.Length > 0)
                outUserDto = GetUserByEmail(inUserDto);

            if (inUserDto.UserId > 0)
                outUserDto = GetUserByUserId(inUserDto);

            return outUserDto;
        }

        private User GetUserByEmail(User inUserDto)
        {
            return ExecuteQueryDto(new UserByEmailSelectHelper(), inUserDto) as User;
        }

        private User GetUserByUserId(User inUserDto)
        {
            return ExecuteQueryDto(new UserByUserIdSelectHelper(), inUserDto) as User;
        }

        public int UpdateUser(User inUserDto)
        {
            return ExecuteNonQuery(new UserUpdateHelper(), inUserDto);
        }

        public int UpdateUserPasswordHash(User inUserDto)
        {
            return ExecuteNonQuery(new UserPasswordHashUpdateHelper(), inUserDto);
        }

        public int UpdateUserPasswordRecoveryInfo(User inUserDto)
        {
            return ExecuteNonQuery(new UserPasswordRecoveryInfoUpdateHelper(), inUserDto);
        }

        public int DeleteUser(User inUserDto)
        {
            return ExecuteNonQuery(new UserDeleteHelper(), inUserDto);
        }

        #region Deprecated
        //public User GetAdminUser(User inUserDto)
        //{
        //    return ExecuteQueryDto(new AdminUserSelectHelper(), inUserDto) as User;
        //}


        #endregion
    }
}