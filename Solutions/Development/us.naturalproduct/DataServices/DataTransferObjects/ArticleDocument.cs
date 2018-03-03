using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class ArticleDocument: Document
    {

        public ArticleDocument() : base() { }

        private Int32 artDocTypeId;
        private string artDocTypeDescription;

        public string ArtDocTypeDescription
        {
            get { return this.artDocTypeDescription; }
            set { this.artDocTypeDescription = value; }
        }

        public Int32 ArtDocTypeId
        {
            get { return this.artDocTypeId; }
            set { this.artDocTypeId = value; }
        }
    }
}
