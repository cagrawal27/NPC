using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCUpdateHelpers
{
    public class VolumeUpdateHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            Volume volumeUpdateDto = criteria as Volume;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spUpdateVolume");

            cw.AddInParameter("VolumeId", DbType.Int32, volumeUpdateDto.VolumeId);

            cw.AddInParameter("VolumeName", DbType.String, volumeUpdateDto.VolumeName);

            cw.AddInParameter("VolumeYear", DbType.String, volumeUpdateDto.VolumeYear);

            cw.AddInParameter("Active", DbType.Boolean, volumeUpdateDto.IsActive);

            cw.AddInParameter("UpdateUserId", DbType.Int32, volumeUpdateDto.UpdateUserId);

            return cw;
        }

    }
}
