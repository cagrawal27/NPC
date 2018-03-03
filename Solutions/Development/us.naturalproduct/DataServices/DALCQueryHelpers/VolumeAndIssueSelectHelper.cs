using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class VolumeAndIssueSelectHelper: DALCHelper
    {

        public override DbCommandWrapper InitializeCommand(Microsoft.Practices.EnterpriseLibrary.Data.Database db, MN.Enterprise.Base.DataTransferObject criteria)
        {
            Issue issueDto = criteria as Issue;

            //Create a database command object within which T-SQL commands can 
            //be executed.
            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetVolumeAndIssueName");

            cw.AddInParameter("VolumeIssueId", DbType.Int32, issueDto.VolumeIssueId);
            
            return cw;
        }

        public override MN.Enterprise.Base.DataTransferObject ConvertResultsDto(DbCommandWrapper cw, IDataReader reader)
        {
            Issue issueDto = null;

            if (reader.Read())
            {
                issueDto = new Issue();

                issueDto.ParentVolume.VolumeName = (string)reader["VolumeName"];

                issueDto.ParentVolume.VolumeYear = (string)reader["VolumeYear"];

                issueDto.IssueName = (string)reader["IssueName"];
            }

            return issueDto;

        }
    }
}
