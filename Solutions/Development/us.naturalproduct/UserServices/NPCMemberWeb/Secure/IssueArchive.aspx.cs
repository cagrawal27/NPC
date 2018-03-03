using System;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    public partial class IssueArchive : BasePage
    {
        private string volumeId;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Issue Archive";

            volumeId = Request.QueryString["volumeId"];

            LoadIssues(volumeId);
        }

        private void LoadIssues(string volumeId)
        {
            if (WebUtils.IsNumeric(volumeId))
            {
                dto.IssueArchive arch = IssueFacade.GetActiveIssues(Convert.ToInt32(volumeId));

                lblVolume.Text = string.Format("Volume {0} ({1})", arch.VolumeDto.VolumeName, arch.VolumeDto.VolumeYear);

                rptrIssues.DataSource = arch.IssueList;

                rptrIssues.DataBind();
            }
        }

        protected void rptrIssues_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //If the item bound is a list item
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                HyperLink lnkIssuePage = (HyperLink) e.Item.FindControl("lnkIssuePage");

                //Get the bound data item
                dto.Issue issue = (dto.Issue) e.Item.DataItem;

                if (!(lnkIssuePage == null || issue == null))
                {
                    lnkIssuePage.Text = string.Format("{0} {1}", "Issue", issue.IssueName);

                    lnkIssuePage.NavigateUrl = string.Format("{0}?volumeissueid={1}",
                                                             Pages.Issue,
                                                             issue.VolumeIssueId);
                }
            }
        }
    }
}