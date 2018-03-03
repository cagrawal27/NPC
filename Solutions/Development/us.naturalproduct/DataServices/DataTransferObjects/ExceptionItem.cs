using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class ExceptionItem
    {
        public ExceptionItem() { }

        private string formVars;
        private string referrer;
        private string queryString;
        private Int32 eventId;
        private Exception exceptionDetails;

        public string FormVars
        {
            get { return this.formVars; }
            set { this.formVars = value; }

        }

        public string Referrer
        {
            get { return referrer; }
            set { referrer = value; }
        }

        public string QueryString
        {
            get { return queryString; }
            set { queryString = value; }
        }

        public int EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        public Exception ExceptionDetails
        {
            get { return exceptionDetails; }
            set { exceptionDetails = value; }
        }

    }
}
