using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class Login: BaseObject
    {
        public Login() { }
        
        private string emailAddress;
        private string password;
        private string passwordHash;
        private string passwordSalt;
        private string ipAddress;

        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
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

        override public bool IsValid()
        {
            return
                !(
                (passwordHash == null || passwordHash.Length.Equals(0)) 
                ||
                (passwordSalt == null || passwordSalt.Length.Equals(0))
                );
        }

    }
}
