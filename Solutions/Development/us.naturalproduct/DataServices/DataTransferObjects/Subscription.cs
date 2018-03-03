using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    /// <summary>
    /// This class encapsulates all information pertaining to a NPC
    /// subscription.
    /// </summary>
    /// <author>Parag Jagdale</author>
    // Revision History
    //
    //=============================================================================
    // Change   Initial Date        Description
    //      1   MN      12/30/2006  Added additional fields and corresponding 
    //                              getters/setters for: volume name, issue name,
    //                              article name, effectiveDate and expirationDate.
    //=============================================================================
    public class Subscription: BaseObject
    {
        public Subscription() : base() {
            
        }

        private Int32 subscriptionId;
        private Int32 volumeIssueId;
        private Int32 articleId;
        private Int32 userId;
               
        private string volumeName;
        private string issueName;
        private string articleName;
        private DateTime effectiveDate;
        private DateTime expirationDate;

        public string VolumeName
        {
            get { return volumeName; }
            set { volumeName = value; }
        }

        public string IssueName
        {
            get { return issueName; }
            set { issueName = value; }
        }

        public string ArticleName
        {
            get { return articleName; }
            set { articleName = value; }
        }

        public DateTime EffectiveDate
        {
            get { return effectiveDate; }
            set { effectiveDate = value; }
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }


        public Int32 SubscriptionId
        {
            get { return this.subscriptionId; }
            set { this.subscriptionId = value; }
        }

        public Int32 VolumeIssueId
        {
            get { return this.volumeIssueId; }
            set { this.volumeIssueId = value; }
        }

        public Int32 ArticleId
        {
            get { return this.articleId; }
            set { this.articleId = value; }
        }

        public Int32 UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

    }
}
