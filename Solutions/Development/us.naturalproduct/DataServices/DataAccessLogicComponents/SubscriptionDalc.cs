using System.Collections.Generic;
using MN.Enterprise.Data;
using us.naturalproduct.DALCDeleteHelpers;
using us.naturalproduct.DALCInsertHelpers;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class SubscriptionDalc : CommonDALC
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SubscriptionDalc() : base()
        {
        }

        /// <summary>
        /// Constructor with a transaction.
        /// </summary>
        /// <param name="transaction"></param>
        public SubscriptionDalc(DALCTransaction transaction)
            : base(transaction)
        {
        }

        #endregion

        public int InsertSubscription(Subscription subscriptionDto)
        {
            return ExecuteNonQuery(new SubscriptionInsertHelper(), subscriptionDto);
        }

        public List<Subscription> GetUserSubscriptions(Subscription subscriptionDto)
        {
            return ExecuteQueryList<Subscription>(new SubscriptionListSelectHelper(), subscriptionDto);
        }

        public List<Article> GetUserAvailableSubscriptions(Subscription subscriptionDto)
        {
            return ExecuteQueryList<Article>(new SubscriptionAvailableListSelectHelper(), subscriptionDto);
        }

        /// <summary>
        /// Updates an existing subscription record in the subscription table</summary>
        /// <returns>The number of rows affected.</returns>
        public int UpdateSubscription(Subscription subscriptionDto)
        {
            return ExecuteNonQuery(new SubscriptionUpdateHelper(), subscriptionDto);
        }

        /// <summary>
        /// Deletes an existing subscription record from the subscription table        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public int DeleteSubscription(Subscription subscriptionDto)
        {

            return ExecuteNonQuery(new SubscriptionDeleteHelper(), subscriptionDto);
        }
    }
}