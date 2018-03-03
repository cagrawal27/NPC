using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.web.Masters;
using dto = us.naturalproduct.DataTransferObjects;
using MN.Enterprise.Business;

namespace us.naturalproduct.web
{
    public partial class AddVolume : BaseAdminPage
    {
        VolumeFacade VolumeBF;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Set title
            ((Secure) Master).PageTitle = "Add Issue";

            //Init business layer
            VolumeBF = new VolumeFacade();

        }

        #region EVENT HANDLERS

        protected void btnAddVolume_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                dto.Volume volumeDto = new dto.Volume();

                volumeDto.VolumeName = tbVolumeName.Text.Trim();

                volumeDto.VolumeYear = tbVolumeYear.Text.Trim();

                volumeDto.IsActive = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));

                volumeDto.CreationUserId = UserInfo.UserDto.UserId;

                VolumeFacade facade = new VolumeFacade(BusinessFacadeBehavior.TRANSACTIONAL);

                dto.ActionStatus status = facade.InsertVolume(volumeDto);

                if (status.IsSuccessful)
                {
                    ResetFormValues();
                }
                
                LblStatus.Text = GetFormattedMessages(status);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetFormValues();
        }

        #endregion

        private void ResetFormValues()
        {
            tbVolumeName.Text = string.Empty;

            tbVolumeYear.Text = string.Empty;

            ddlStatus.SelectedIndex = 0;
        }
    }
}