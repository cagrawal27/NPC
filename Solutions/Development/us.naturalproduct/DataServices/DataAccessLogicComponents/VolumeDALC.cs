using System.Collections.Generic;
using MN.Enterprise.Data;
using us.naturalproduct.DALCInsertHelpers;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DALCUpdateHelpers;
using us.naturalproduct.DALCDeleteHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class VolumeDALC: CommonDALC
    {
        public VolumeDALC()
        {
        }

        public VolumeDALC(DALCTransaction transaction)
            : base(transaction)
        {
        }

        public Volume Insert(Volume insertVolumeDto)
        {
            return ExecuteNonQueryDto(new VolumeInsertHelper(), insertVolumeDto) as Volume;
        }

        public Volume Select(Volume selectVolumeDto)
        {
            return ExecuteQueryDto(new VolumeSelectHelper(), selectVolumeDto) as Volume;
        }

        public int Delete(Volume deleteVolumeDto)
        {
            return ExecuteNonQuery(new VolumeDeleteHelper(), deleteVolumeDto);
        }

        public int Update(Volume updateVolumeDto)
        {
            return ExecuteNonQuery(new VolumeUpdateHelper(), updateVolumeDto);
        }

    }
}
