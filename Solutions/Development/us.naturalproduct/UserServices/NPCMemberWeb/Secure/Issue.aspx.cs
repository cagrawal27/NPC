using System;
using System.Web.UI.WebControls;

using us.naturalproduct.Common;
using us.naturalproduct.web.Masters;
using us.naturalproduct.BusinessServices.BusinessFacades;
using dto = us.naturalproduct.DataTransferObjects;



namespace us.naturalproduct.web
{
    public partial class Issue : BasePage
    {
        private string _VolumeIssueId;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Issue Details";

            _VolumeIssueId = ValidateQryParamInt32("VolumeIssueId");

            if (_VolumeIssueId == null)
                Response.Redirect(Pages.Home);
            else
            {
                    IssueFacade issueFacade = new IssueFacade();

                    dto.Issue issueDto = new dto.Issue();

                    issueDto.VolumeIssueId = Convert.ToInt32(_VolumeIssueId);
                                        
                    issueDto = issueFacade.GetUserArticles(UserInfo.UserDto, issueDto);

                    if (issueDto == null)
                        Response.Redirect(Pages.Home, true);
                    else
                    {
                        LoadPageValues(issueDto);
                    }
            }

        }

        private void LoadPageValues(dto.Issue issueDto)
        {
            HdrVolume.InnerText = string.Format("Volume {0} {1}", issueDto.ParentVolume.VolumeName, issueDto.ParentVolume.VolumeYear);

            HdrIssue.InnerText = issueDto.IssueName;

            rptrIssueDocs.DataSource = issueDto.Documents;

            rptrIssueDocs.DataBind();

            rptrArticles.DataSource = issueDto.Articles;

            rptrArticles.DataBind();
        }

        protected void rptrArticles_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                Repeater rptrDocs = (Repeater)e.Item.FindControl("rptrDocs");

                dto.Article articleDto = e.Item.DataItem as dto.Article;

                if (null != articleDto)
                {
                    rptrDocs.DataSource = articleDto.Documents;

                    rptrDocs.DataBind();                   
                }
            }
        }
    }
}