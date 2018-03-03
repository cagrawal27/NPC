using System;
using System.Data;
using us.naturalproduct.DataAccessObjects;

namespace us.naturalproduct.web
{
    public partial class ViewDoc : BasePage
    {
        private static Int32 ONE_KB = 1024;

        protected void Page_Load(object sender, EventArgs e)
        {
            string docId = Request.QueryString["docid"];

            if (WebUtils.IsNumeric(docId))
                LoadDoc(Convert.ToInt32(docId));
        }

        private void LoadDoc(Int32 docId)
        {
            IDataReader rdr = DocumentDao.GetDocument(docId);

            if (rdr.Read())
            {
                Response.ContentType = "application/pdf";

                Response.AppendHeader("Content-Disposition", string.Format("attachment; Filename = {0}.pdf", "NPC"));

                byte[] buffer = new byte[ONE_KB];

                long idx = 0, size = 0;

                //Write the BLOB chunk by chunk.
                while ((size = rdr.GetBytes(1, idx, buffer, 0, ONE_KB)) == ONE_KB)
                {
                    Response.BinaryWrite(buffer);
                    idx += ONE_KB;
                }

                //Write the last bytes.
                if (size > 0)
                {
                    byte[] remaining = new byte[size];

                    Array.Copy(buffer, 0, remaining, 0, size);

                    Response.BinaryWrite(remaining);
                }

                Response.Flush();

                Response.Close();
            }

            rdr.Close();
        }
    }
}