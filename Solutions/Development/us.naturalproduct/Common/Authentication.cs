using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;

namespace us.naturalproduct.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Authentication
    {
        private const int SALT_SIZE = 10;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal static string CreateSalt(Int32 size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            //Init a byte array for storing the crypto random number
            byte[] buff = new byte[size];
            
            //Fill the byte array with crypto random number
            rng.GetBytes(buff);
            
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSalt()
        {
            return CreateSalt(SALT_SIZE);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordClearText"></param>
        /// <returns></returns>
        public static string GenerateRandomSaltedHash(string plainText)
        {
            return GenerateSaltedHash(plainText, GetRandomSalt()); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GenerateSaltedHash(string plainText, string salt)
        {
            //Add salt to the plain text password
            string plainTextWithSalt = String.Concat(plainText, salt);

            //Hash the password using MD5
            string hashedText =
                  FormsAuthentication.HashPasswordForStoringInConfigFile(plainTextWithSalt, "MD5");

            return hashedText;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string GenerateSimpleHash(string plainText)
        {
            //Hash the text using MD5
            string hashedText =
                  FormsAuthentication.HashPasswordForStoringInConfigFile(plainText, "MD5");

            return hashedText;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordFromUser"></param>
        /// <param name="passwordFromDB"></param>
        /// <returns></returns>
        public static bool DoPasswordsMatch(string passwordFromUser, string passwordFromDB, string saltFromDB)
        {
            //Hash the user password with the extracted salt
            string hashedPasswordFromUser = GenerateSaltedHash(passwordFromUser, saltFromDB);

            //Return evaluation
            return passwordFromDB.Equals(hashedPasswordFromUser);
        }

        public static bool DoesHashedTextMatch(string plainText, string hashedText)
        {
            string hashedTextFromPlainText = GenerateSimpleHash(plainText);

            return hashedText.Equals(hashedTextFromPlainText);
        }

        public static bool DoesHashedTextMatch(string plainText, string hashedText, string saltFromDB)
        {
            string hashedPlainTextFromUser = GenerateSaltedHash(plainText, saltFromDB);

            return hashedText.Equals(hashedPlainTextFromUser);
        }
    }
}
