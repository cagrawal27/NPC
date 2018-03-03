using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.QueryHelpers;

namespace us.naturalproduct.DataAccessObjects
{
    public class VolumeDao
    {
        public static List<Volume> GetActiveVolumes()
        {
            List<Volume> volumeList = new List<Volume>();

            Volume vol;

            try
            {
                DataTable volumeTbl = VolumeQueryHelper.GetActiveVolumes();

                if (volumeTbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in volumeTbl.Rows)
                    {
                        vol = new Volume();

                        vol.VolumeId = (Int32) dr["VolumeId"];

                        vol.VolumeName = (string) dr["VolumeName"];

                        vol.VolumeYear = (string) dr["VolumeYear"];

                        volumeList.Add(vol);
                    }
                }

                return volumeList;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the volume list.", sqlEx);
            }
        }


        public static DataTable AdminGetVolumes()
        {
            try
            {
                return VolumeQueryHelper.AdminGetVolumes();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the volume list.", sqlEx);
            }
        }
    }
}