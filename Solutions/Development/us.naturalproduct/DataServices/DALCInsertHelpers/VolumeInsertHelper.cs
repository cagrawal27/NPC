using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCInsertHelpers
{
    public class VolumeInsertHelper: DALCHelper
    {
        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject insertObj)
        {
            Volume volumeDto = insertObj as Volume;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spInsertVolume");

            //Parameters are added here, update each line to reflect the parameter from
            //the subscription object
            cw.AddOutParameter("VolumeId", DbType.Int32, 4);

            cw.AddInParameter("VolumeName", DbType.String, volumeDto.VolumeName);

            cw.AddInParameter("VolumeYear", DbType.String, volumeDto.VolumeYear);

            cw.AddInParameter("Active", DbType.Boolean, volumeDto.IsActive);

            cw.AddInParameter("CreationUserId", DbType.Int32, volumeDto.CreationUserId);

            //Return the commandwrapper object to DALCHelper where the stored proc
            //will be executed
            return cw;
        }

        public override DataTransferObject CreateResultsDto(DbCommandWrapper cw, DataTransferObject insertObj)
        {
            Volume volumeDto = insertObj as Volume;

            volumeDto.VolumeId = Convert.ToInt32(cw.GetParameterValue("VolumeId"));

            return volumeDto;
        }
    }
}
