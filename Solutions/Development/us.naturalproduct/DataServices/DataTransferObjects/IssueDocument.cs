using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class IssueDocument: Document
    {
        public IssueDocument() : base() { }

        private Int32 issueDocTypeId;
        private string issueDocTypeDescription;

        public string IssueDocTypeDescription
        {
            get { return this.issueDocTypeDescription; }
            set { this.issueDocTypeDescription = value; }
        }

        public Int32 IssueDocTypeId
        {
            get { return this.issueDocTypeId; }
            set { this.issueDocTypeId = value; }
        }
    }
}
