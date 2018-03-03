using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class Article : BaseObject
    {
        public Article() : base() {

            documents = new List<ArticleDocument>();
            
            volumeDto = new Volume();

            issueDto = new Issue();
        }

        
        private Int32 articleId;
        private string authors;
        private string title;
        private string keywords;
        private Int32 pageNumber;
        private List<ArticleDocument> documents;

        private Volume volumeDto;
        private Issue issueDto;

        public Volume VolumeDto
        {
            get { return this.volumeDto; }
            set { this.volumeDto = value; }
        }

        public Issue IssueDto
        {
            get { return this.issueDto; }
            set { this.issueDto = value; }
        }

        public List<ArticleDocument> Documents
        {
            get { return this.documents; }
            set { this.documents = value; }
        }

        public int ArticleId
        {
            get { return articleId; }
            set { articleId = value; }
        }

        public string Authors
        {
            get { return authors; }
            set { authors = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        public Int32 PageNumber
        {
            get { return this.pageNumber; }
            set { this.pageNumber = value; }
        }
    }

}
