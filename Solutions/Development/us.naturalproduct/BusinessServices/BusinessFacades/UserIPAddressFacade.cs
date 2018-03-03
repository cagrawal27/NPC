using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Web.Security;
using MN.Enterprise.Base;
using MN.Enterprise.Business;
using us.naturalproduct.Common;
using us.naturalproduct.DataAccessLogicComponents;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessFacades
{
    public class UserIPAddressFacade: BusinessFacade
    {
        public UserIPAddressFacade():base(BusinessFacadeBehavior.NONE)
        {
        }

        public UserIPAddressFacade(int businessFacadeBehavior):base(businessFacadeBehavior)
        {
        }

        public List<UserIPAddress> GetList(UserIPAddress inUserIPAddressDto)
        {
            List<UserIPAddress> userIPAddresses = null;

            try
            {
                UserIPAddressDalc dalc = new UserIPAddressDalc();

                userIPAddresses = dalc.GetList(inUserIPAddressDto);
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

            return userIPAddresses;
        }

        public UserIPAddress GetUserByIP(UserIPAddress inUserIPAddressDto)
        {
            UserIPAddress outUserIPAddressDto = null;

            try
            {
                UserIPAddressDalc dalc = new UserIPAddressDalc();

                outUserIPAddressDto = dalc.GetUserByIP(inUserIPAddressDto);
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

            return outUserIPAddressDto;
            
        }

        public ActionStatus Exists(UserIPAddress inUserIPAddressDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                UserIPAddressDalc dalc = new UserIPAddressDalc();

                bool Exists = dalc.Exists(inUserIPAddressDto);

                status.IsSuccessful = !Exists;
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

            if (!status.IsSuccessful)
                status.Messages.Add(
                    new ActionMessage("An IP Address that matches this range already exists."));

            return status;            
        }

        public ActionStatus Add(UserIPAddress inUserIPAddressDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                UserIPAddressDalc dalc = new UserIPAddressDalc();

                //Start tran
                Start();

                dalc.Add(inUserIPAddressDto);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                status.Messages.Add(new ActionMessage("IP Address successfully added."));
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
                    new ActionMessage("Could not add IP Address."));

            return status;
        }

        public ActionStatus Delete(UserIPAddress inUserIPAddressDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                UserIPAddressDalc dalc = new UserIPAddressDalc();

                dalc.Delete(inUserIPAddressDto);

                status.IsSuccessful = true;

                status.Messages.Add(new ActionMessage("IP Address deleted successfully."));
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

            if (!status.IsSuccessful)
                status.Messages.Add(
                    new ActionMessage("Could not delete IP Address."));

            return status;            
        }
    }
}
