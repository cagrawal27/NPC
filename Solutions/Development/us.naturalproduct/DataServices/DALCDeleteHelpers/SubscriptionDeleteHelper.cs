using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;


namespace us.naturalproduct.DALCDeleteHelpers
{
    public class SubscriptionDeleteHelper: DALCHelper
    {
        //TODO:  12-30-2006 - MN - Implement this class for deleting subscription records
        //Coding is complete.  Use as reference
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            //Cast the DataTransferObject to the Subscription object
            Subscription subscriptionDto = criteria as Subscription;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminDeleteSubscription");

            //Parameters are added here, update each line to reflect the parameter from
            //the subscription object
            cw.AddInParameter("SubscriptionId", DbType.Int32, subscriptionDto.SubscriptionId);

            //Return the commandwrapper object to DALCHelper where the stored proc
            //will be executed
            return cw;
        }
    }
}
