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
    public class UserFacade : BusinessFacade
    {
        public UserFacade() : base(BusinessFacadeBehavior.NONE)
        {
        }

        public UserFacade(int businessFacadeBehavior) : base(businessFacadeBehavior)
        {
        }
      
        public User GetUser(User inUserDto, bool includeRoles)
        {
            User outUserDto = null;

            try
            {
                UserDalc userDalc = new UserDalc();

                outUserDto = userDalc.GetUser(inUserDto);

                if (includeRoles)
                {
                    RoleDalc roleDalc = new RoleDalc();

                    inUserDto.UserId = outUserDto.UserId;

                    outUserDto.RolesList = roleDalc.GetRoleList(inUserDto);
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

            return outUserDto;            
        }

        public User GetUser(User inUserDto)
        {
            User outUserDto = null;

            try
            {
                UserDalc userDalc = new UserDalc();

                outUserDto = userDalc.GetUser(inUserDto);
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

            return outUserDto;
        }

        public bool VerifyPasswordAnswer(User inUserDto)
        {
            bool doAnswersMatch = false;


            try
            {
                UserDalc userDalc = new UserDalc();

                User outUserDto = userDalc.GetUser(inUserDto);

                if (inUserDto.SecretQuestionId == outUserDto.SecretQuestion1Id)
                {
                    doAnswersMatch =
                        Authentication.DoesHashedTextMatch(inUserDto.SecretAnswer, outUserDto.SecretAnswer1Hash);
                }
                else if (inUserDto.SecretQuestionId == outUserDto.SecretQuestion2Id)
                {
                    doAnswersMatch =
                        Authentication.DoesHashedTextMatch(inUserDto.SecretAnswer, outUserDto.SecretAnswer2Hash);
                }
                else
                    throw new MNException("Password recovery information not available for user.");
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

            return doAnswersMatch;
        }

        public ActionStatus DeleteUser(User inUserDto)
        {
            ActionStatus status = new ActionStatus();


            try
            {
                UserDalc userDalc = new UserDalc(GetTransaction());

                //Start tran
                Start();

                userDalc.DeleteUser(inUserDto);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                status.Messages.Add(new ActionMessage("User successfully deleted."));
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw ex;
            }

            return status;
        }

        public ActionStatus ResetPassword(User inUserDto)
        {
            ActionStatus status = new ActionStatus();


            try
            {
                UserDalc userDalc = new UserDalc(GetTransaction());

                //Start tran
                Start();

                //Get the password salt
                User outUserDto = userDalc.GetUser(inUserDto);

                //Generate a new password
                string newPassword = Membership.GeneratePassword(10, 0);

                //Generate a hash from the new password and salt
                inUserDto.PasswordHash = Authentication.GenerateSaltedHash(newPassword, outUserDto.PasswordSalt);

                //Set the account status to stale so that users have to change the password
                inUserDto.AccountStatus = Constants.Account_Status_Stale;

                //Update the password
                userDalc.UpdateUserPasswordHash(inUserDto);

                //Create a new mail message
                MailMessage msg = new MailMessage();

                //Set the subject
                msg.Subject = string.Format(ConfigurationManager.AppSettings["EmailSubject"], "Password Reset");

                //Set the to address
                msg.To.Add(inUserDto.EmailAddress);

                string msgBody = ConfigurationManager.AppSettings["ResetPassEmail"];

                msg.IsBodyHtml = true;

                //set the message body
                msg.Body = string.Format(msgBody, inUserDto.EmailAddress,
                                         newPassword);

                //Init a new smtpclient
                SmtpClient client = new SmtpClient();

                //Use the client to send the message
                client.Send(msg);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                status.Messages.Add(
                    new ActionMessage(
                        string.Format("Password was successfully reset and emailed to {0}", inUserDto.EmailAddress)));
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                //abort tran

                SetAbort();

                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw ex;
            }

            if (!status.IsSuccessful)
                status.Messages.Add(new ActionMessage("Failed to reset password."));

            return status;
        }

        public void UpdateUser(User inUserDto)
        {
            try
            {
                //Begin transaction
                Start();

                UserDalc userDalc = new UserDalc(GetTransaction());

                userDalc.UpdateUser(inUserDto);

                //Commit transaction
                SetComplete();
            }
            catch (MNException ex)
            {
                //Abort transaction
                SetAbort();

                //TODO:  Log error
                throw ex;
            }
            catch (Exception ex)
            {
                //Abort transaction
                SetAbort();

                //TODO:  Log error
                throw ex;
            }
        }

        public ActionStatus UpdatePassword(User inUserDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                UserDalc userDalc = new UserDalc(GetTransaction());

                //Start tran
                Start();

                User outUserDto = userDalc.GetUser(inUserDto);

                inUserDto.PasswordHash = Authentication.GenerateSaltedHash(inUserDto.Password, outUserDto.PasswordSalt);

                inUserDto.AccountStatus = Constants.Account_Status_Active;

                userDalc.UpdateUserPasswordHash(inUserDto);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                status.Messages.Add(new ActionMessage("Your password has been successfully changed."));
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw ex;
            }

            if (!status.IsSuccessful)
                status.Messages.Add(
                    new ActionMessage("Could not change your password.  Please contact the system administrator."));

            return status;
        }

        public ActionStatus UpdatePasswordRecoveryInfo(User inUserDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                UserDalc userDalc = new UserDalc(GetTransaction());

                //Start tran
                Start();

                inUserDto.SecretAnswer1Hash = Authentication.GenerateSimpleHash(inUserDto.SecretAnswer1);

                inUserDto.SecretAnswer2Hash = Authentication.GenerateSimpleHash(inUserDto.SecretAnswer2);

                userDalc.UpdateUserPasswordRecoveryInfo(inUserDto);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                status.Messages.Add(
                    new ActionMessage("Your password recovery information has been successfully changed."));
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw ex;
            }

            if (!status.IsSuccessful)
                status.Messages.Add(
                    new ActionMessage(
                        "Could not change your password recovery information.  Please contact the system administrator."));

            return status;
        }
    }
}