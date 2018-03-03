using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class ActionMessage
    {
        public ActionMessage(bool isError, Int32 msgCode, string msgDetail)
        {
            this.isError = isError;
            this.msgCode = msgCode;
            this.msgDetail = msgDetail;
        }

        public ActionMessage(string msgDetail)
        {
            this.msgDetail = msgDetail;
        }
        
        private bool isError;

        private Int32 msgCode;

        private string msgDetail;

        public bool IsError
        {
            get { return this.isError; }
            set { this.isError = value; }
        }
        public Int32 MsgCode { 
            get { return this.msgCode; }
            set { this.msgCode = value; }
        }

        public string MsgDetail
        {
            get { return this.msgDetail; }
            set { this.msgDetail = value; }
        }

    }
}
