using System;
using System.Diagnostics;
using System.IO;
using System.Web.UI.WebControls;
using us.naturalproduct.Common;

/// <summary>
/// Summary description for BaseAdminPage
/// </summary>
namespace us.naturalproduct.web
{
    public class BaseAdminPage : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            Debug.WriteLine("In BaseAdminPage.Page_Load");

            if (!UserInfo.IsUserAdmin())
                Response.Redirect(Pages.Home, false);

            base.OnLoad(e);
        }

        protected string SaveDocToTemp(FileUpload tbUploadedDocument)
        {
            // Get size of uploaded file
            int fileLen = tbUploadedDocument.PostedFile.ContentLength;

            // Allocate a buffer for reading of the file
            byte[] docData = new byte[fileLen];

            string filePathName;

            //Create the file name with path
            filePathName = string.Format("{0}\\{1})({2}",
                                         Server.MapPath("..\\..\\Tmp_Data"),
                                         Session.SessionID,
                                         Path.GetFileName(tbUploadedDocument.PostedFile.FileName));

            // Read uploaded file from the Stream
            tbUploadedDocument.PostedFile.InputStream.Read(docData, 0, fileLen);

            //Write the file to the tmp directory
            WebUtils.WriteToFile(filePathName, ref docData);

            return filePathName;
        }

        protected void DeleteTempDocs()
        {
            string tmpPath = Server.MapPath("..\\..\\Tmp_Data");

            string[] files = Directory.GetFiles(tmpPath, "*");

            if (files != null || files.Length > 0)
            {
                for (int j = 0; j < files.Length; j++)
                {
                    if (files[j].IndexOf(Session.SessionID).Equals(-1))
                    {
                        DateTime timeCreated = File.GetCreationTime(files[j]);

                        if (timeCreated < DateTime.Now.AddMinutes(-15.0))
                            File.Delete(files[j]);
                    }
                    else
                        File.Delete(files[j]);
                }
            }
        }
    }
}