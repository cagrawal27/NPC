using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;


namespace us.naturalproduct.DALCQueryHelpers
{
    public class VolumeSelectHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Microsoft.Practices.EnterpriseLibrary.Data.Database db, MN.Enterprise.Base.DataTransferObject criteria)
        {
            Volume volSelectDto = criteria as Volume;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetVolume");

            cw.AddInParameter("VolumeId", DbType.Int32, volSelectDto.VolumeId);

            return cw;
        }

        public override MN.Enterprise.Base.DataTransferObject ConvertResultsDto(DbCommandWrapper cw, System.Data.IDataReader reader)
        {
            Volume volumeDto = null;

            if (reader.Read())
            {
                volumeDto = new Volume();

                volumeDto.VolumeName = Convert.ToString(reader["VolumeName"]);

                volumeDto.VolumeYear = Convert.ToString(reader["VolumeYear"]);

                volumeDto.IsActive = Convert.ToBoolean(reader["Active"]);

                volumeDto.CreationUserId = Convert.ToInt32(reader["CreationUserId"]);

                volumeDto.CreationDateTime = Convert.ToDateTime(reader["CreationDateTime"]);

                volumeDto.UpdateUserId = Convert.ToInt32(reader["UpdateUserId"]); 

                volumeDto.UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"]);
            }

            return volumeDto;
        }

    }
}
