using System;
using System.Configuration;
using System.Web.UI;

namespace us.naturalproduct.web.Masters
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string PageTitle
        {
            get { return this.Page.Title; }

            set
            {
                this.Page.Title = string.Format("{0} - {1}",
                                                ConfigurationSettings.AppSettings["SiteTitle"],
                                                value);
            }
        }

        //public string GetAbsoluteURLPath(string PageURL)
        //{
        //    // Form an absolute path using the server name and v-dir name
        //    string serverName =
        //           HttpUtility.UrlEncode(this.Page.Request.ServerVariables["SERVER_NAME"]);

        //    string vdirName = this.Page.Request.ApplicationPath;

        //    return string.Format("http://{0}{1}/{2}", serverName, vdirName, PageURL);
        //}

        //public DataTable GetCountries(bool bypassCache)
        //{
        //    if (IsCachingEnabled())
        //    {
        //        string cacheKey = "Countries";

        //        DataTable tblCountries = Cache.Get(cacheKey) as DataTable;

        //        if (bypassCache || tblCountries == null)
        //        {
        //            //tblCountries = CountryLookup.GetCountryTable();
        //            tblCountries = ReferenceDataFacade.GetCountries();

        //            Cache.Insert(cacheKey, tblCountries, null, GetCacheExpireTimeFromConfig(), System.Web.Caching.Cache.NoSlidingExpiration);
        //        }

        //        return tblCountries;            
        //    }

        //   // return CountryLookup.GetCountryTable();
        //    return ReferenceDataFacade.GetCountries();
        //}

        //public DataTable GetStates(bool bypassCache)
        //{
        //    if (IsCachingEnabled())
        //    {
        //        string cacheKey = "States";

        //        DataTable tblStates = Cache.Get(cacheKey) as DataTable;

        //        if (bypassCache || tblStates == null)
        //        {
        //            tblStates = Constants.GetStates();

        //            Cache.Insert(cacheKey, tblStates, null, GetCacheExpireTimeFromConfig(), System.Web.Caching.Cache.NoSlidingExpiration);
        //        }

        //        return tblStates;   
        //    }

        //    return Constants.GetStates();
        //}

        //public bool IsCachingEnabled()
        //{
        //    string boolString = ConfigurationSettings.AppSettings["CacheEnabled"];

        //    return bool.Parse(boolString);
        //}

        //public DateTime GetCacheExpireTimeFromConfig()
        //{
        //    string absExpireTime = ConfigurationSettings.AppSettings["AbsCacheTime"];

        //    if (absExpireTime == null) 
        //        absExpireTime = "4";

        //    return DateTime.Now.AddHours(Double.Parse(absExpireTime));
        //}

        //public string GetMessages(ActionStatus status)
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
    }
}