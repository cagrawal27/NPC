using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class IssueArchive: BaseObject
    {
        public IssueArchive() : base() {
            
            volumeDto = new Volume();

            issueList = new List<Issue>();
        }

        public IssueArchive(Volume argVolume, List<Issue> argIssueList): base()
        {
            this.volumeDto = argVolume;

            this.issueList = argIssueList;
        }

        private Volume volumeDto;
        
        private List<Issue> issueList;

        public Volume VolumeDto
        {
            get { return this.volumeDto; }
            set { this.volumeDto = value; }
        }

        public List<Issue> IssueList
        {
            get { return this.issueList; }
            set { this.issueList = value; }
        }

    }
}
