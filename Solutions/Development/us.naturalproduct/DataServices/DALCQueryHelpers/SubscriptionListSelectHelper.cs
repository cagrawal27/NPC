using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    //TODO:  12-30-2006 - MN - Implement this class for returning subscription record(s)
    public class SubscriptionListSelectHelper : DALCHelper<Subscription>
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            Subscription subscriptionDto = criteria as Subscription;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminGetSubscriptions");

            //TODO:  Add the appropriate parameter to the commandwrapper object here
            cw.AddInParameter("UserId", DbType.Int32, subscriptionDto.UserId);
            
            return cw;
        }

        public override List<Subscription> ConvertResultsList(DbCommandWrapper cw, System.Data.IDataReader reader)
        {
            //Once the query is executed the results must converted to a 
            //array of Subscription objects
        
            //Check if reader is null.  No results returned
            if (reader == null)
                return null;

            List<Subscription> subscriptions = new List<Subscription>();

            while (reader.Read())
            {
                //Init new subscription object for each subscription record
                Subscription subscriptionDto = new Subscription();

                //Populate subscription object from record
                subscriptionDto.SubscriptionId = (Int32) reader["SubscriptionId"];
                subscriptionDto.VolumeIssueId = Convert.ToInt32(reader["VolumeIssueId"]);
                subscriptionDto.ArticleId = Convert.ToInt32(reader["ArticleId"]);
                subscriptionDto.VolumeName = (string) reader["VolumeName"];
                subscriptionDto.IssueName = (string)reader["IssueName"];
                subscriptionDto.ArticleName = ((string)reader["ArticleName"]).Substring(0, 15);
                subscriptionDto.EffectiveDate = (DateTime)reader["EffectiveDate"];
                subscriptionDto.ExpirationDate = (DateTime)reader["ExpirationDate"];
                subscriptionDto.IsActive = (bool)reader["Active"];

                //Add to list array
                subscriptions.Add(subscriptionDto);
            }

            if (subscriptions.Count == 0)
                return null;

            return subscriptions;
        }

    }
}
