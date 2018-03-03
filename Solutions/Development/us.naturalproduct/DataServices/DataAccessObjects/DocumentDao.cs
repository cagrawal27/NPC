using System;
using System.Data;
using System.Data.SqlClient;
using us.naturalproduct.QueryHelpers;

namespace us.naturalproduct.DataAccessObjects
{
    public class DocumentDao
    {
        public static IDataReader GetDocument(Int32 docId)
        {
            try
            {
                return DocumentQueryHelper.GetDocument(docId);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.ToString());

                throw new DataException("An exception occured getting the document.", sqlEx);
            }
        }
    }
}