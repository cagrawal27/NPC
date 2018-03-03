using System;
using System.IO;

namespace us.naturalproduct.DataTransferObjects
{
    public class Document : BaseObject
    {
        public Document() : base()
        {
        }

        #region Private Members
        private Int32 docId;
        private string fullFileName;
        private string comments;
        private byte[] data;
        private bool isDeleted;
        private Int32 fileSizeKB;

        #endregion

        #region Public Properties
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public int FileSizeKB
        {
            get
            {
                return fileSizeKB;
            }
            set
            {
                fileSizeKB = value;        
            }
        }

        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public string FullFileName
        {
            get { return fullFileName; }
            set { fullFileName = value; }
        }

        public string FileName
        {
            get
            {
                if (fullFileName == null || fullFileName.Length == 0)
                    throw new InvalidOperationException("FileName is not set");
                else
                {
                    string fileName = Path.GetFileName(fullFileName);

                    Int32 pos = fileName.IndexOf(")(");

                    if (pos.Equals(-1))
                        return fileName;

                    return fileName.Substring(pos + 2);
                }
            }
        }

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        #endregion 

        #region Public Methods

        public void LoadData()
        {
            FileStream fsReader = new FileStream(FullFileName, FileMode.Open, FileAccess.Read);

            BufferedStream bs = new BufferedStream(fsReader);

            byte[] docData = new byte[bs.Length];

            int bytesRead = bs.Read(docData, 0, docData.Length);

            if (bytesRead > 0)
                data = docData;

            bs.Close();
        }

        public int CalculateFileSizeInKB()
        {
            if (data == null)
                return 0;

            return data.Length/1024;
        }

        #endregion
    }
}