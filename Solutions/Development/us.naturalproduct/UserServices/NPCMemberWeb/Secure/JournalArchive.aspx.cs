using System;
using System.Web.UI.WebControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class JournalArchive : BasePage
    {
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Secure) Master).PageTitle = "Journal Archive";

            LoadVolumes();
        }

        private void LoadVolumes()
        {
            rptrVolumes.DataSource = VolumeFacade.GetActiveVolumes();

            rptrVolumes.DataBind();
        }


        protected void rptrVolumes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //If the item bound is a list item
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                HyperLink lnkVolume = (HyperLink) e.Item.FindControl("lnkVolume");

                dto.Volume volume = (dto.Volume) e.Item.DataItem;

                if (!(lnkVolume == null || volume == null))
                {
                    lnkVolume.Text = string.Format("Volume {0} ({1})", volume.VolumeName, volume.VolumeYear);

                    lnkVolume.NavigateUrl = string.Format("{0}?volumeid={1}",
                                                          Pages.IssueArchive,
                                                          volume.VolumeId);
                }
            }
        }
    }
}