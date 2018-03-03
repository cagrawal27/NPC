using System;
using System.Collections.Generic;
using System.Data;

using MN.Enterprise.Business;
using MN.Enterprise.Base;

using us.naturalproduct.DataAccessLogicComponents;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessServices.BusinessFacades
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
    //      1       MN  01/04/2006  Modified class to inherit BusinessFacade.
    //                              
    //=============================================================================
    public class IssueFacade : BusinessFacade
    {
        public IssueFacade() : base(BusinessFacadeBehavior.NONE)
        {
        }

        public IssueFacade(int businessFacadeBehavior) : base(businessFacadeBehavior)
        {
        }

        private Issue issueDto;

        public Issue IssueDto
        {
            get { return issueDto; }
            set { issueDto = value; }
        }

        public static IssueArchive GetActiveIssues(Int32 VolumeId)
        {
            return IssueDao.GetActiveIssues(VolumeId);
        }

        public ActionStatus AddIssue()
        {
            ActionStatus status = IssueDao.AddIssue(issueDto);

            return status;
        }

        public ActionStatus UpdateIssue()
        {
            ActionStatus status = IssueDao.UpdateIssue(issueDto);

            if (status.IsSuccessful)
                status.Messages.Add(new ActionMessage(false, 1, "Successfully updated issue."));
            else
                status.Messages.Add(new ActionMessage(false, 1, "Failed to update issue."));

            return status;
        }

        public static Issue AdminGetIssue(Int32 VolumeIssueId)
        {
            return IssueDao.AdminGetIssue(VolumeIssueId);
        }

        public static DataTable AdminGetIssues(Int32 VolumeId)
        {
            return IssueDao.AdminGetIssues(VolumeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto">User object.  Must have UserId set.</param>
        /// <param name="inIssueDto">Issue object.  Must have VolumeIssueId set.</param>
        /// <returns></returns>
        public Issue GetUserArticles(User userDto, Issue inIssueDto)
        {
            Issue outIssueDto = null;

            try
            {
                IssueDalc issueDalc = new IssueDalc();

                //Get the volume name, volume year and issue name
                outIssueDto = issueDalc.GetIssueAndVolumeName(inIssueDto);

                //Get the Issue Documents
                IssueDocumentDalc issueDocumentDalc = new IssueDocumentDalc();

                Subscription subscriptionDto = new Subscription();

                subscriptionDto.UserId = userDto.UserId;

                subscriptionDto.VolumeIssueId = inIssueDto.VolumeIssueId;

                outIssueDto.Documents = issueDocumentDalc.GetIssueDocs(subscriptionDto);

                //Get the articles
                ArticleDalc articleDalc = new ArticleDalc();

                outIssueDto.Articles = articleDalc.GetActiveArticles(inIssueDto);

                if (null != outIssueDto.Articles && outIssueDto.Articles.Count > 0)
                {
                    //Get articledocuments for each article
                    ArticleDocumentDalc articleDocDalc = new ArticleDocumentDalc();

                    foreach (Article article in outIssueDto.Articles)
                    {
                        subscriptionDto.ArticleId = article.ArticleId;

                        article.Documents = articleDocDalc.GetArticleDocs(subscriptionDto);
                    }
                }
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error

                throw;
            }
            catch (Exception ex)
            {
                //TODO:  Log error

                throw;
            }

            return outIssueDto;
        }
    }
}