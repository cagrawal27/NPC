using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class User: BaseObject
    {
        public User():base() {
            
            IPAddresses = new List<string>();
        
        }

        private Int32 userId;
        private string emailAddress;
        private Int32 accountStatus;
        private Int32 accountType;
        private string firstName;
        private string lastName;
        private string middleInitial;
        private Int32 secretQuestion1Id;
        private Int32 secretQuestion2Id;
        private Int32 secretQuestionId;
        private string secretAnswer;
        private string secretAnswer1Hash;
        private string secretAnswer2Hash;
        private string secretAnswer1;
        private string secretAnswer2;
        private string passwordSalt;
        private string password;
        private string passwordHash;
        private List<Role> rolesList;

        public List<Role> RolesList
        {
            get { return rolesList; }
            set { rolesList = value; }
        }

        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int SecretQuestionId
        {
            get { return secretQuestionId; }
            set { secretQuestionId = value; }
        }

        public string SecretAnswer
        {
            get { return secretAnswer; }
            set { secretAnswer = value; }
        }

        public string PasswordSalt
        {
            get { return passwordSalt; }
            set { passwordSalt = value; }
        }

        public string SecretAnswer1Hash
        {
            get { return secretAnswer1Hash; }
            set { secretAnswer1Hash = value; }
        }

        public string SecretAnswer2Hash
        {
            get { return secretAnswer2Hash; }
            set { secretAnswer2Hash = value; }
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

        //private string[] roles;
        private List<string> ipAddresses;
        
        public Int32 SecretQuestion1Id
        {
            get { return secretQuestion1Id; }
            set { secretQuestion1Id = value; }
        }

        public Int32 SecretQuestion2Id
        {
            get { return secretQuestion2Id; }
            set { secretQuestion2Id = value; }
        }

        public string[] Roles
        {
            get
            {
                List<string> strArray = new List<string>();

                if (rolesList != null && rolesList.Count > 0)
                {                   
                    foreach(Role role in rolesList)
                        strArray.Add(role.RoleId.ToString());
                }

                return strArray.ToArray();
            }
            //set { this.roles = value; }
        }

        private GenericPrincipal principalObj;

        public GenericPrincipal PrincipalObj
        {
            get { return this.principalObj; }
            set { this.principalObj = value; }
        }

        public Int32 UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
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

        public List<string> IPAddresses
        {
            get
            {
                return ipAddresses;
            }
            set
            {
                ipAddresses = value;
            }
        }
    }
}
