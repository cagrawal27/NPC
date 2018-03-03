using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    /// <summary>
    /// Summary description for BasePage
    /// </summary>
    public class BasePage : Page
    {
        public static ListItem liDefault = new ListItem("-- Select --", "");


        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("In BasePage.Page_Load");

        }

        protected override void OnInit(EventArgs e)
        {
            RegisterAnalyticsScript();
        }

        protected void RegisterAnalyticsScript()
        {
            if (!ClientScript.IsStartupScriptRegistered("AnalyticsID"))
            {
                //Register the script
                string javaScript = string.Format("var AnalyticsID = \"{0}\";", ConfigurationSettings.AppSettings["AnalyticsID"]);

                ClientScript.RegisterStartupScript(GetType(), "AnalyticsID", javaScript, true);
            }

            if (!ClientScript.IsStartupScriptRegistered("AnalyticsScript"))
            {
                //Register the script
                StreamReader sr = new StreamReader(Server.MapPath(Resources.GoogleAnalytics));

                string javaScript = sr.ReadToEnd();

                sr.Close();

                ClientScript.RegisterStartupScript(GetType(), "AnalyticsScript", javaScript);
            }
        }

        protected string ValidateQryParamInt32(string name)
        {
            string value = Request.QueryString[name];

            bool success = false;

            int result = 0;

            if (value != null)
                success = Int32.TryParse(value, out result);

            if (success)
                return result.ToString();

            return null;
        }

        protected string GetAbsoluteURLPath(string PageURL)
        {
            // Form an absolute path using the server name and v-dir name
            string serverName =
                HttpUtility.UrlEncode(Page.Request.ServerVariables["SERVER_NAME"]);

            string vdirName = Page.Request.ApplicationPath;

            return string.Format("http://{0}{1}/{2}", serverName, vdirName, PageURL);
        }

        protected DataTable GetCountries(bool bypassCache)
        {
            if (IsCachingEnabled())
            {
                string cacheKey = "Countries";

                DataTable tblCountries = Cache.Get(cacheKey) as DataTable;

                if (bypassCache || tblCountries == null)
                {
                    //tblCountries = CountryLookup.GetCountryTable();
                    tblCountries = ReferenceDataFacade.GetCountries();

                    Cache.Insert(cacheKey, tblCountries, null, GetCacheExpireTimeFromConfig(), Cache.NoSlidingExpiration);
                }

                return tblCountries;
            }

            // return CountryLookup.GetCountryTable();
            return ReferenceDataFacade.GetCountries();
        }

        protected DataTable GetStates(bool bypassCache)
        {
            if (IsCachingEnabled())
            {
                string cacheKey = "States";

                DataTable tblStates = Cache.Get(cacheKey) as DataTable;

                if (bypassCache || tblStates == null)
                {
                    tblStates = Constants.GetStates();

                    Cache.Insert(cacheKey, tblStates, null, GetCacheExpireTimeFromConfig(), Cache.NoSlidingExpiration);
                }

                return tblStates;
            }

            return Constants.GetStates();
        }

        protected bool IsCachingEnabled()
        {
            string boolString = ConfigurationSettings.AppSettings["CacheEnabled"];

            return bool.Parse(boolString);
        }

        protected DateTime GetCacheExpireTimeFromConfig()
        {
            string absExpireTime = ConfigurationSettings.AppSettings["AbsCacheTime"];

            if (absExpireTime == null)
                absExpireTime = "4";

            return DateTime.Now.AddHours(Double.Parse(absExpireTime));
        }

        protected string GetFormattedMessages(string message)
        {
            ActionStatus status = new ActionStatus();

            status.Messages.Add(new ActionMessage(message));

            return GetFormattedMessages(status);
        }

        protected string GetFormattedMessages(string message, bool isError)
        {
            ActionStatus status = new ActionStatus();

            status.IsSuccessful = !isError;

            status.Messages.Add(new ActionMessage(message));

            return GetFormattedMessages(status);
        }

        protected string GetFormattedMessages(ActionStatus status)
        {
            StringBuilder sbMsgs = new StringBuilder();

            if (status.Messages.Count > 0)
            {
                string begin = "<span class=\"{0}\">{1}: \n";

                if (status.IsSuccessful)
                    sbMsgs.Append(string.Format(begin, "Success", "Status"));
                else
                    sbMsgs.Append(string.Format(begin, "Failure", "The following errors occured"));

                sbMsgs.Append("<ul>");

                foreach (ActionMessage msg in status.Messages)
                {
                    //sbMsgs.Append(string.Format("{0} - {1} - {2}", msg.IsError, msg.MsgCode, msg.MsgDetail));
                    sbMsgs.Append(string.Format("<li>{0}</li>", msg.MsgDetail));
                }

                sbMsgs.Append("</ul></span>");
            }

            return sbMsgs.ToString();
        }

        protected void DisplayPopup(string msg)
        {
            InjectClientScript(string.Format("alert('{0}');", msg));
        }

        protected void InjectClientScript(string jsCode)
        {
            Response.Write("<script  type=\"text/javascript\">");
            Response.Write("<!--");
            Response.Write(jsCode);
            Response.Write("// -->");
            Response.Write("</script>");
        }

        protected bool ConvIntToBool(string val)
        {
            return true;
        }

        protected bool IsUserAdmin()
        {
            User userDto = Session["UserDto"] as User;
            bool isAdmin = false;

            if (userDto == null)
                throw new InvalidOperationException("User information has not been populated in the session.");
            else
            {
                return userDto.PrincipalObj.IsInRole(Constants.Role_Admin.ToString());
            }

            return isAdmin;
        }

        #region Deprecated/Unused Code
        //protected void LoadUserInfo()
        //{
        //    UserFacade facade = new UserFacade();

        //    dto.User inUserDto = new dto.User();

        //    inUserDto.EmailAddress = Page.User.Identity.Name;

        //    dto.User userDto = facade.GetUser(inUserDto, true);

        //    userDto.PrincipalObj = new GenericPrincipal(User.Identity, userDto.Roles);

        //    Session["UserDto"] = userDto;
        //}
        //protected string GetFormattedErrorMessage(bool isError, string errorMessage)
        //{
        //    string formattedError =
        //        string.Format(@"{0}: \n<ul><li>{1}</li></ul>",
        //                      (isError ? "The following errors occured" : "Status"),
        //                      errorMessage);

        //    return formattedError;
        //}
        //protected string GetMessages(ActionStatus status)
        //{
        //    StringBuilder sbMsgs = new StringBuilder();

        //    if (status.Messages.Count > 0)
        //    {
        //        if (status.IsSuccessful)
        //        {
        //            sbMsgs.Append("Status: \n");
        //            sbMsgs.Append("<ul>");
        //        }
        //        else
        //        {
        //            sbMsgs.Append("The following errors occured: \n");
        //            sbMsgs.Append("<ul>");
        //        }

        //        foreach (ActionMessage msg in status.Messages)
        //        {
        //            //sbMsgs.Append(string.Format("{0} - {1} - {2}", msg.IsError, msg.MsgCode, msg.MsgDetail));
        //            sbMsgs.Append(string.Format("<li>{0}</li>", msg.MsgDetail));
        //        }

        //        sbMsgs.Append("</ul>");
        //    }

        //    return sbMsgs.ToString();
        //}

        //protected string GetStatusMessages(ActionStatus status, bool isError)
        //{
        //    StringBuilder sbMsgs = new StringBuilder();

        //    if (status.Messages.Count > 0)
        //    {
        //        sbMsgs.Append("The following errors occured: \n");
        //        sbMsgs.Append("<ul>");


        //        foreach (ActionMessage msg in status.Messages)
        //        {
        //            //sbMsgs.Append(string.Format("{0} - {1} - {2}", msg.IsError, msg.MsgCode, msg.MsgDetail));
        //            sbMsgs.Append(string.Format("<li>{0}</li>", msg.MsgDetail));
        //        }

        //        sbMsgs.Append("</ul>");
        //    }

        //    return sbMsgs.ToString();
        //}

        #endregion
    }
}