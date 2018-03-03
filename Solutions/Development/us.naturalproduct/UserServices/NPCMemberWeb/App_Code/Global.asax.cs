using System;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.UpdateHelpers;

namespace us.naturalproduct.web
{
    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public partial class Global : HttpApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        public Global()
        {
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            string boolString = ConfigurationSettings.AppSettings["ExceptionLogging"];

            //Logg error only if logging enabled
            if (bool.Parse(boolString))
            {
                ExceptionItem ex = new ExceptionItem();

                HttpContext ctx = HttpContext.Current;

                string referrer, sForm, qryStr;

                referrer = sForm = qryStr = string.Empty;

                if (ctx.Request.ServerVariables["HTTP_REFERER"] != null)
                    referrer = ctx.Request.ServerVariables["HTTP_REFERER"].ToString();

                ex.Referrer = referrer;

                sForm = (ctx.Request.Form != null) ? ctx.Request.Form.ToString() : string.Empty;

                ex.FormVars = sForm;

                qryStr = (ctx.Request.QueryString != null) ? ctx.Request.QueryString.ToString() : string.Empty;

                ex.QueryString = qryStr;

                ex.ExceptionDetails = Server.GetLastError().GetBaseException();

                ExceptionInsertHelper.Execute(ex);
            }
        }

        protected void Session_End(Object sender, EventArgs e)
        {
        }

        protected void Application_End(Object sender, EventArgs e)
        {
        }
    }
}