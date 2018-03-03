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
    public class SubscriptionAvailableListSelectHelper : DALCHelper<Article>
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            Subscription subscriptionDto = criteria as Subscription;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spAdminGetAvailableSubscriptions");

            //TODO:  Add the appropriate parameter to the commandwrapper object here
            cw.AddInParameter("UserId", DbType.Int32, subscriptionDto.UserId);

            cw.AddInParameter("VolumeIssueId", DbType.Int32, subscriptionDto.VolumeIssueId);

            return cw;
        }

        public override List<Article> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {
            //Once the query is executed the results must converted to a 
            //array of Subscription objects

            //Check if reader is null.  No results returned
            if (reader == null)
                return null;

            List<Article> articles = new List<Article>();

            while (reader.Read())
            {
                //Init new subscription object for each subscription record
                Article articleDto = new Article();

                //Populate subscription object from record
                articleDto.ArticleId = (Int32)reader["ArticleId"];

                articleDto.Title = (string)reader["Title"];

                //Add to list array
                articles.Add(articleDto);
            }

            if (articles.Count == 0)
                return null;

            return articles;
        }
    }
}