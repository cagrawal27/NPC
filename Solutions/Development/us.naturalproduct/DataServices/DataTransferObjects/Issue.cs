using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    /// <summary>
    /// This class is a DataAccessLogicComponent for retrieving Issue related data.
    /// It calls specific Issue related helper classes.
    /// </summary>
    /// <author>Monish Nagisetty</author>
    /// <created>05/01/2006</created>
    /// <updated>01/04/2007</updated>
    // Revision History
    //
    //=============================================================================
    // Change   Initial Date        Description
    //      1       MN  01/04/2006  Commented private member and public accessor for
    //                              VolumeId.  Added ParentVolume member to 
    //                              represent the parent volume object for this issue
    //                              object.
    //=============================================================================
    public class Issue: BaseObject
    {

        public Issue() : base() {
            parentVolume = new Volume();
            documents = new List<IssueDocument>();
            articles = new List<Article>();
        }

        #region Private Members
        //private Int32 volumeId;
        private Int32 volumeIssueId;
        private Int32 issueId;
        private string issueName;
        private List<IssueDocument> documents;
        private List<Article> articles;
        private Volume parentVolume;

        #endregion

        #region Public Accessors
        //public Int32 VolumeId
        //{
        //    get { return this.volumeId; }
        //    set { this.volumeId = value; }
        //}
        public Volume ParentVolume
        {
            get { return parentVolume; }
            set { parentVolume = value; }
        }

        public List<Article> Articles
        {
            get { return articles; }
            set { articles = value; }
        }

        public List<IssueDocument> Documents
        {
            get { return this.documents; }
            set { this.documents = value; }
        }

        public Int32 VolumeIssueId
        {
            get { return this.volumeIssueId; }
            set { this.volumeIssueId = value; }
        }


        public Int32 IssueId
        {
            get { return this.issueId; }
            set { this.issueId = value; }
        }

        public string IssueName
        {
            get { return this.issueName; }
            set { this.issueName = value; }
        }
        #endregion
    }
}
