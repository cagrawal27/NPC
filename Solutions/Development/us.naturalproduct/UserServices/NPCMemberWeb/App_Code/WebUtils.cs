using System;
using System.IO;
using System.Text.RegularExpressions;

namespace us.naturalproduct.web
{
    public class WebUtils
    {
        /// <summary>
        /// Check whether the provided string is a strong password.
        /// The string must contain at least one uppercase, one lowercase,
        /// one numeral, and one special character.
        /// The method allows uppercase, lowercase, digits,
        /// and keyboard characters except for: \ < > "
        /// </summary>
        /// <param name="password">The password tovalidate.</param>
        /// <returns>True if the password is a strong password, false otherwise.</returns>
        public static bool IsStrongPassword(String password)
        {
            // Special Characters (update here then cut & paste to 2 locations below)
            // \-\+\?\*\$\[\]\^\.\(\)\|`~!@#%&_ ={}:;  ',/

            // Defines minimum appearance of characters
            String ex1 =
                @"
              ^        # anchor at the start
              (?=.*\d)    # must contain at least one digit
              (?=.*[a-z])    # must contain at least one lowercase
              (?=.*[A-Z])    # must contain at least one uppercase
              (?=.*[\-\+\?\*\$\^\.\|!@#%&_=,])  # must contain at least one special character
              .{8,15}    # min, max length
              $        # anchor at the end";

            // Allow only defined characters
            String ex2 =
                @"
              ^        # anchor at the start
              [\w\-\+\?\*\$\^\.\|!@#%&_ =,] # alphanumerics and special characters only
              {8,15}      # min, max length
              $        # anchor at the end";

            return (IsMatch(password, ex1, RegexOptions.IgnorePatternWhitespace) &&
                    IsMatch(password, ex2, RegexOptions.IgnorePatternWhitespace));
        }

        /// <summary>
        /// Match a regular expression against a provided string.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="pattern">The regular expression pattern used to
        /// validate the input.</param>
        /// <param name="options">A bitwise OR combination of the
        /// RegExOption enumeration values</param>
        /// <returns>True if the parameters produce a match, false
        /// otherwise.</returns>
        public static bool IsMatch(String input, String pattern, RegexOptions options)
        {
            Regex regex = new Regex(pattern, options);
            Match m = regex.Match(input);
            if (m.Success)
                return true;
            else
                return false;
        }

        public static bool IsNumeric(string number)
        {
            if (number == null || number.Length == 0)
                return false;

            foreach (Char c in number)
                if (!Char.IsNumber(c))
                    return false;

            return true;
        }

        // Writes file to given path
        public static void WriteToFile(string strPath, ref byte[] Buffer)
        {
            // Create a file
            FileStream docFs = new FileStream(strPath, FileMode.Create);

            // Write data to the file
            docFs.Write(Buffer, 0, Buffer.Length);

            // Close file
            docFs.Close();
        }
    }
}