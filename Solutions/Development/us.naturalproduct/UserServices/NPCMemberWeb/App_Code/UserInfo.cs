using System.Security.Principal;
using System.Web;
using System.Web.UI;
using us.naturalproduct.BusinessServices.BusinessFacades;
using us.naturalproduct.Common;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.web
{
    /// <summary>
    /// Summary description for UserInfo
    /// </summary>
    public sealed class UserInfo
    {
        #region Properties

        public static User UserDto
        {
            get { return HttpContext.Current.Session["UserInfo"] as User; }
        }

        private static User userDto
        {
            set { HttpContext.Current.Session["UserInfo"] = value; }
        }

        #endregion

        public static void Init(IPrincipal principal)
        {
            UserFacade facade = new UserFacade();

            User inUserDto = new User();

            inUserDto.EmailAddress = principal.Identity.Name;

            User userData = facade.GetUser(inUserDto, true);

            userData.PrincipalObj = new GenericPrincipal(principal.Identity, userData.Roles);

            userDto = userData;

            //Set the userid so that it can be accessed in the forms
            HttpContext.Current.Session["UserId"] = userData.UserId;
        }

        public static bool IsInitialized()
        {
            if (UserDto == null || UserDto.PrincipalObj == null)
                return false;

            return true;
        }

        public static bool IsAccountStale()
        {
            if (IsInitialized())
                return (UserDto.AccountStatus == Constants.Account_Status_Stale);

            return false;
        }

        public static bool IsUserAdmin()
        {
            if (IsInitialized())
                return UserDto.PrincipalObj.IsInRole(Constants.Role_Admin.ToString());

            return false;
        }

        public static void Abandon()
        {
            userDto = null;
        }
    }
}