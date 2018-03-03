using System;
using System.Collections.Generic;
using System.Text;
using MN.Enterprise.Data;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class IssueDocumentDalc: CommonDALC
    {
        public IssueDocumentDalc()
        {
        }

        public IssueDocumentDalc(DALCTransaction transaction) : base(transaction)
        {
        }

        public List<IssueDocument> GetIssueDocs(Subscription subscriptionDto)
        {
            return ExecuteQueryList(new IssueDocumentListBySubscriptionSelectHelper(), subscriptionDto);
        }
    }
}
