using us.naturalproduct.Common;
using us.naturalproduct.DataAccessObjects;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class RegistrationFacade
    {
        private Registration registrationDto;

        public Registration RegistrationDto
        {
            get { return this.registrationDto; }
            set { this.registrationDto = value; }
        }

        public ActionStatus RegisterUser()
        {
            PrepareRegistrationDto();

            RegistrationDao regDao = new RegistrationDao();

            ActionStatus status = regDao.RegisterUser(registrationDto);

            return status;
        }

        private void PrepareRegistrationDto()
        {
            //Create random salt
            registrationDto.PasswordSalt = Authentication.GetRandomSalt();

            //Create random password hash from the supplied password and salt
            registrationDto.PasswordHash =
                Authentication.GenerateSaltedHash(registrationDto.Password, registrationDto.PasswordSalt);

            registrationDto.SecretAnswer1Hash = Authentication.GenerateSimpleHash(registrationDto.SecretAnswer1);

            registrationDto.SecretAnswer2Hash = Authentication.GenerateSimpleHash(registrationDto.SecretAnswer2);

            registrationDto.AddressTypeId = Constants.Address_Type_Default;

            registrationDto.AccountStatus = Constants.Account_Status_Active;

            registrationDto.CreationUserId = Constants.Anonymous_Web_User_Id;

            registrationDto.DefaultRoleId = Constants.Role_Member;
        }
    }
}