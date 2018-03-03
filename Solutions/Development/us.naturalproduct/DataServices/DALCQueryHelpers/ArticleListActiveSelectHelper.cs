using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class ArticleListActiveSelectHelper : DALCHelper<Article>
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            Issue issueDto = criteria as Issue;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetActiveArticles");

            cw.AddInParameter("VolumeIssueId", DbType.Int32, issueDto.VolumeIssueId);

            return cw;
        }

        public override List<Article> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {
            //Check if reader is null.  No results returned
            if (reader == null)
                return null;

            List<Article> articles = new List<Article>();

            while (reader.Read())
            {
                //Init new Article object for each Article record
                Article articleDto = new Article();

                //Populate Article object from record
                articleDto.ArticleId = (Int32) reader["ArticleId"];

                articleDto.Title = (string) reader["Title"];

                articleDto.Authors = (string) reader["Authors"];

                articleDto.Keywords = (string) reader["Keywords"];

                articleDto.CreationDateTime = Convert.ToDateTime(reader["CreationDateTime"]);

                //Add to list array
                articles.Add(articleDto);
            }

            if (articles.Count == 0)
                return null;

            return articles;
        }
    }
}