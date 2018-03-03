using System;
using System.Collections.Generic;
using System.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Business;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.DataAccessLogicComponents;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class VolumeFacade: BusinessFacade
    {
        public VolumeFacade() : base(BusinessFacadeBehavior.NONE)
        {
        }

        public VolumeFacade(int businessFacadeBehavior)
            : base(businessFacadeBehavior)
        {
        }


        public static List<Volume> GetActiveVolumes()
        {
            return VolumeDao.GetActiveVolumes();
        }

        public static DataTable AdminGetVolumes()
        {
            return VolumeDao.AdminGetVolumes();
        }

        public ActionStatus InsertVolume(Volume insertDto)
        {
            ActionStatus status = new ActionStatus();

            try
            {
                VolumeDALC dalc = new VolumeDALC(GetTransaction());

                //Start tran
                Start();

                dalc.Insert(insertDto);

                //commit tran
                SetComplete();

                status.IsSuccessful = true;

                status.Messages.Add(new ActionMessage("Volume successfully added."));
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error
                //abort tran
                SetAbort();

                throw ex;
            }

            return status;
        }
    }
}