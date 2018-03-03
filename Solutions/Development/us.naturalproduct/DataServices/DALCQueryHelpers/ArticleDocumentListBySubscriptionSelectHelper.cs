using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;


namespace us.naturalproduct.DALCQueryHelpers
{
    public class ArticleDocumentListBySubscriptionSelectHelper : DALCHelper<ArticleDocument>
    {
        public override DbCommandWrapper InitializeCommand(Database db,
                                                           DataTransferObject criteria)
        {
            Subscription subscriptionDto = criteria as Subscription;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw =
                DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetArticleDocumentsBySubscription");

            cw.AddInParameter("UserId", DbType.Int32, subscriptionDto.UserId);

            cw.AddInParameter("VolumeIssueId", DbType.Int32, subscriptionDto.VolumeIssueId);

            cw.AddInParameter("ArticleId", DbType.Int32, subscriptionDto.ArticleId);

            return cw;
        }

        public override List<ArticleDocument> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {
            if (reader == null)
                return null;

            List<ArticleDocument> articleDocuments = new List<ArticleDocument>();

            while (reader.Read())
            {
                //Init new subscription object for each subscription record
                ArticleDocument articleDocumentDto = new ArticleDocument();

                //Populate ArticleDocument object from record
                articleDocumentDto.ArtDocTypeId = (Int32) reader["ArtDocTypeId"];

                articleDocumentDto.ArtDocTypeDescription = (string) reader["ArtDocTypeDescription"];

                articleDocumentDto.DocId = (Int32) reader["DocId"];

                articleDocumentDto.FileSizeKB = (Int32) reader["FileSizeKB"];

                //Add to list array
                articleDocuments.Add(articleDocumentDto);
            }

            if (articleDocuments.Count == 0)
                return null;

            return articleDocuments;
        }
    }
}