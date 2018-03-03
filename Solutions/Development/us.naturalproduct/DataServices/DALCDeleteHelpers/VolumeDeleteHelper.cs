using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCDeleteHelpers
{
    public class VolumeDeleteHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            Volume volDeleteDto = criteria as Volume;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spDeleteVolume");

            cw.AddInParameter("VolumeId", DbType.Int32, volDeleteDto.VolumeId);

            return cw;
        }
    }
}
