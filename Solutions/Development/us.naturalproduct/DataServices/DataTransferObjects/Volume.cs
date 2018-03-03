using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class Volume: BaseObject
    {
        public Volume():base() { }

        public Volume(Int32 argVolumeId, string argVolumeName, string argVolumeYear)
        {
            this.volumeId = argVolumeId;

            this.volumeName = argVolumeName;

            this.volumeYear = argVolumeYear;
        }

        private Int32 volumeId;
        private string volumeName;
        private string volumeYear;

        public Int32 VolumeId
        {
            get { return this.volumeId; }
            set { this.volumeId = value; }
        }

        public string VolumeName 
        {
            get { return this.volumeName; }
            set { this.volumeName = value; }
        }

        public string VolumeYear
        {
            get { return this.volumeYear; }
            set { this.volumeYear = value; }
        }
    }
}
