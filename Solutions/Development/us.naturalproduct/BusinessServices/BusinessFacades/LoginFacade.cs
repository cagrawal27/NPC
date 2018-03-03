using System;
using System.Configuration;
using System.Net.Mail;
using System.Web.Security;
using MN.Enterprise.Base;
using MN.Enterprise.Business;
using us.naturalproduct.Common;
using us.naturalproduct.DataAccessLogicComponents;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class LoginFacade : BusinessFacade
    {
        public LoginFacade() : base(BusinessFacadeBehavior.NONE)
        {
        }

        public ActionStatus AuthenticateUser(Login inLoginDto)
        {
            ActionStatus status = new ActionStatus();

            //Login loginCred = loginDao.GetUserCredentials(inLoginDto.EmailAddress);
            User inUserDto = new User();
            UserFacade facade = new UserFacade();

            inUserDto.EmailAddress = inLoginDto.EmailAddress;

            User outUserDto = facade.GetUser(inUserDto);

            if (outUserDto == null || outUserDto.IsInactive)
            {
                //status flag is false by default
                //so simply add error message

                status.Messages.Add(new ActionMessage(true, 2, "Invalid email and password combination."));
            }
            else
            {
                if (outUserDto.AccountStatus.Equals(Constants.Account_Status_Active))
                {
                    if (Authentication.DoPasswordsMatch(inLoginDto.Password, outUserDto.PasswordHash,
                                                        outUserDto.PasswordSalt))
                    {
                        status.IsSuccessful = true;                        
                    }
                    else
                        status.Messages.Add(new ActionMessage(true, 2, "Invalid email and password combination."));

                }
                else if (outUserDto.AccountStatus.Equals(Constants.Account_Status_Locked))
                    status.Messages.Add(new ActionMessage(true, 2, "Your account has been deactivated."));
                else if (outUserDto.AccountStatus.Equals(Constants.Account_Status_Stale))
                    status.Messages.Add(new ActionMessage(true, 2, "Your account password is stale.  Please contact the system administrator to reset your password."));
            }

            return status;
        }

        public Login AuthenticateIP(Login inLoginDto)
        {
            if (null == inLoginDto)
                throw new ArgumentNullException("inLoginDto");

            if (null == inLoginDto.IPAddress)
                throw new ArgumentNullException("IPAddress");

            Login outLoginDto = null;

            try
            {
                string[] octets = inLoginDto.IPAddress.Split(new char[] {'.'});

                UserIPAddress inUserIPAddressDto = new UserIPAddress();

                inUserIPAddressDto.BeginAddress.Octet1 = Convert.ToInt32(octets[0]);

                inUserIPAddressDto.BeginAddress.Octet2 = Convert.ToInt32(octets[1]);

                inUserIPAddressDto.BeginAddress.Octet3 = Convert.ToInt32(octets[2]);

                inUserIPAddressDto.BeginAddress.Octet4 = Convert.ToInt32(octets[3]);

                UserIPAddressDalc dalc = new UserIPAddressDalc();

                UserIPAddress outUserIPAddressDto = dalc.GetUserByIP(inUserIPAddressDto);

                if (outUserIPAddressDto != null)
                {
                    UserFacade userFacade = new UserFacade();

                    User inUserDto = new User();

                    inUserDto.UserId = outUserIPAddressDto.UserId;

                    User outUserDto = userFacade.GetUser(inUserDto);

                    if (outUserDto != null)
                    {
                        outLoginDto = new Login();

                        outLoginDto.EmailAddress = outUserDto.EmailAddress;
                    }
                }
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error

                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error

                throw ex;
            }

            return outLoginDto;
        }

        #region Deprecated

        //public Login AuthenticateIP()
        //{
        //    LoginDao loginDao = new LoginDao();

        //    Login loginCred = loginDao.GetIPCredentials(loginDto.IPAddress);

        //    return loginCred;
        //}

        #endregion
    }
}