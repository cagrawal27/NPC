using System.Data;
using us.naturalproduct.QueryHelpers;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class ReferenceDataFacade
    {
        public static DataTable GetCountries()
        {
            return ReferenceQueryHelper.GetCountries();
        }

        public static DataTable GetArticleDocTypes()
        {
            return ReferenceQueryHelper.GetArticleDocTypes();
        }

        public static DataTable GetIssueDocTypes()
        {
            return ReferenceQueryHelper.GetIssueDocTypes();
        }
    }
}