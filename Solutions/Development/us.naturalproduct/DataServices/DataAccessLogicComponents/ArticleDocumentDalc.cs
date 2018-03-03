using System;
using System.Collections.Generic;
using System.Text;
using MN.Enterprise.Data;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class ArticleDocumentDalc: CommonDALC
    {
        public ArticleDocumentDalc()
        {
        }

        public ArticleDocumentDalc(DALCTransaction transaction) : base(transaction)
        {
        }

        public List<ArticleDocument> GetArticleDocs(Subscription subscriptionDto)
        {
            return ExecuteQueryList(new ArticleDocumentListBySubscriptionSelectHelper(), subscriptionDto);
        }
    }
}
