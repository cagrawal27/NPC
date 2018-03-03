using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCInsertHelpers
{
    //TODO:  12-30-2006 - MN - Implement this class for inserting subscription records
    public class SubscriptionInsertHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            //Cast the DataTransferObject to the Subscription object
            Subscription subscriptionDto = criteria as Subscription;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminInsertSubscription");

            //Parameters are added here, update each line to reflect the parameter from
            //the subscription object
            cw.AddInParameter("UserId", DbType.Int32, subscriptionDto.UserId);

            cw.AddInParameter("VolumeIssueId", DbType.Int32, subscriptionDto.VolumeIssueId);

            cw.AddInParameter("ArticleId", DbType.Int32, subscriptionDto.ArticleId);

            cw.AddInParameter("EffectiveDate",DbType.DateTime , subscriptionDto.EffectiveDate);

            cw.AddInParameter("ExpirationDate", DbType.DateTime, subscriptionDto.ExpirationDate);

            cw.AddInParameter("Active", DbType.Boolean, subscriptionDto.IsActive);

            cw.AddInParameter("CreationUserId", DbType.Int32, subscriptionDto.CreationUserId);

            //Return the commandwrapper object to DALCHelper where the stored proc
            //will be executed
            return cw;
        }
    }
}
