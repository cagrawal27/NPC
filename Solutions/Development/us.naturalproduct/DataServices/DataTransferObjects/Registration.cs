using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class Registration: BaseObject
    {
        public Registration():base()
        {
        }

        private string emailAddress;
        private string password;
        private string passwordHash;
        private string passwordSalt;
        private Int32 secretQuestion1;
        private Int32 secretQuestion2;
        private string secretAnswer1;
        private string secretAnswer2;
        private string secretAnswer1Hash;
        private string secretAnswer2Hash;
        private Int32 accountStatus;
        private Int32 accountType;
        private string firstName;
        private string lastName;
        private string middleInitial;
        private string addressLine1;
        private string addressLine2;
        private string city;
        private string state;
        private string zip;
        private Int32 countryId;
        private string phone;
        private string fax;
        private Int32 addressTypeId;
        private Int32 defaultRoleId;

        public Int32 DefaultRoleId
        {
            get { return this.defaultRoleId; }
            set { this.defaultRoleId = value; }
        }

        public string SecretAnswer1Hash
        {
            get { return this.secretAnswer1Hash; }
            set { this.secretAnswer1Hash = value; }
        }

        public string SecretAnswer2Hash
        {
            get { return this.secretAnswer2Hash; }
            set { this.secretAnswer2Hash = value; }
        }

        public Int32 AddressTypeId
        {
            get { return this.addressTypeId; }
            set { this.addressTypeId = value; }
        }

        public Int32 AccountStatus
        {
            get { return this.accountStatus; }
            set { this.accountStatus = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = value; }
        }

        public string PasswordSalt
        {
            get { return this.passwordSalt; }
            set { this.passwordSalt = value; }
        }

        public Int32 SecretQuestion1
        {
            get { return secretQuestion1; }
            set { secretQuestion1 = value; }
        }

        public Int32 SecretQuestion2
        {
            get { return secretQuestion2; }
            set { secretQuestion2 = value; }
        }

        public string SecretAnswer1
        {
            get { return secretAnswer1; }
            set { secretAnswer1 = value; }
        }

        public string SecretAnswer2
        {
            get { return secretAnswer2; }
            set { secretAnswer2 = value; }
        }

        public Int32 AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string MiddleInitial
        {
            get { return middleInitial; }
            set { middleInitial = value; }
        }

        public string AddressLine1
        {
            get { return addressLine1; }
            set { addressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return addressLine2; }
            set { addressLine2 = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public Int32 CountryId
        {
            get { return countryId; }
            set { countryId = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

    }
}
