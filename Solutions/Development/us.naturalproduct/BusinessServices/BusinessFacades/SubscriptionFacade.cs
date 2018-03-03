using System;
using System.Collections.Generic;
using System.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Business;
using us.naturalproduct.DataAccessLogicComponents;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class SubscriptionFacade : BusinessFacade
    {
        public SubscriptionFacade()
            : base(BusinessFacadeBehavior.NONE)
        {
        }

        public SubscriptionFacade(int businessFacadeBehavior)
            : base(businessFacadeBehavior)
        {
        }

        public List<Subscription> GetUserSubscriptions(Subscription subscriptionDto)
        {
            List<Subscription> subscriptions = null;

            try
            {
                SubscriptionDalc userDalc = new SubscriptionDalc();

                subscriptions = userDalc.GetUserSubscriptions(subscriptionDto);

            }
            catch (MNException mnEx)
            {

                throw mnEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return subscriptions;
        }

        public List<Article> GetUserAvailableSubscriptions(Subscription subscriptionDto)
        {
            List<Article> articles = null;

            try
            {
                SubscriptionDalc userDalc = new SubscriptionDalc();

                articles = userDalc.GetUserAvailableSubscriptions(subscriptionDto);
            }
            catch (MNException mnEx)
            {
                throw mnEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return articles;
        }

        public ActionStatus AddSubscriptions(List<Subscription> subscriptions)
        {
            ActionStatus status = new ActionStatus();

            if (subscriptions.Count == 0)
                status.Messages.Add(new ActionMessage("At least one article must be selected"));
            else
            {
                try
                {
                    SubscriptionDalc userDalc = new SubscriptionDalc(GetTransaction());

                    //Start tran
                    Start();

                    foreach (Subscription subscriptionDto in subscriptions)
                        userDalc.InsertSubscription(subscriptionDto);                  

                    //commit tran
                    SetComplete();

                    status.IsSuccessful = true;

                    status.Messages.Add(new ActionMessage("Your subscriptions have been successfully added."));
                }
                catch (MNException mnEx)
                {
                    //TODO:  Log error
                    //Rollback tran
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
            }
            return status;            
        }

        public ActionStatus AddSubscription(Subscription subscriptionDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                SubscriptionDalc userDalc = new SubscriptionDalc(GetTransaction());

                //Start tran
                Start();

                userDalc.InsertSubscription(subscriptionDto);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                //TODO: You might not return this message everytime someone adds
                //a subscription (That could get annoying).
                //The isSuccessful flag might be sufficient
                status.Messages.Add(new ActionMessage("Your subscription has been successfully changed."));
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                //Rollback tran
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

        public ActionStatus UpdateSubscription(Subscription subscriptionDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                SubscriptionDalc sDalc = new SubscriptionDalc();

                //Start transaction
                Start();

                sDalc.UpdateSubscription(subscriptionDto);

                //Commit transaction
                SetComplete();

                status.Messages.Add(new ActionMessage("Your subscription has been successfully updated."));

                status.IsSuccessful = true;
            }
            catch (MNException mnEx)
            {
                // TODO: Log error
                //Abort transaction
                SetAbort();
                throw mnEx;
            }
            catch (Exception ex)
            {
                // TODO: Log error
                //Abort transaction
                SetAbort();
                throw ex;
            }

            return status;
        }

        public ActionStatus DeleteSubscription(Subscription subscriptionDto)
        {
            ActionStatus status = new ActionStatus();
            
            try
            {
                SubscriptionDalc sDalc = new SubscriptionDalc();
                
                // Start transaction
                Start();

                sDalc.DeleteSubscription(subscriptionDto);

                // Commit transaction
                SetComplete();

                status.Messages.Add(new ActionMessage("Your subscription has been successfully deleted."));

                status.IsSuccessful = true;
            }
            catch (MNException mnEx)
            {
                // TODO: Log error
                //Abort transaction
                SetAbort();
                throw mnEx;
            }
            catch (Exception ex)
            {
                // TODO: Log error
                //Abort transaction
                SetAbort();
                throw ex;
            }

            return status;
        }

        public ActionStatus DeleteSubscriptions(List<Subscription> subscriptions)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                SubscriptionDalc sDalc = new SubscriptionDalc();

                // Start transaction
                Start();

                foreach (Subscription subscriptionDto in subscriptions)
                    sDalc.DeleteSubscription(subscriptionDto);                  

                // Commit transaction
                SetComplete();

                status.Messages.Add(new ActionMessage("All subscriptions have been successfully removed."));

                status.IsSuccessful = true;
            }
            catch (MNException mnEx)
            {
                // TODO: Log error
                //Abort transaction
                SetAbort();
                throw mnEx;
            }
            catch (Exception ex)
            {
                // TODO: Log error
                //Abort transaction
                SetAbort();
                throw ex;
            }

            return status;            

        }
    }
}
