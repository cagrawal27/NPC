using System.Collections.Generic;
using MN.Enterprise.Data;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class ArticleDalc : CommonDALC
    {
        public ArticleDalc()
        {
        }

        public ArticleDalc(DALCTransaction transaction) : base(transaction)
        {
        }

        public List<Article> GetActiveArticles(Issue issueDto)
        {
            return ExecuteQueryList(new ArticleListActiveSelectHelper(), issueDto);
        }
    }
}