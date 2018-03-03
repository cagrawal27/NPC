using System;

namespace MN.Enterprise.Utilities
{
	/// <summary>
	/// DataTypeValidator is a class containing utility methods for determing if the given data
	/// conforms to a particular data type, including structured exception handling to prevent
	/// invalid data type errors.
	/// </summary>
	public class DataTypeValidator
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public DataTypeValidator()
		{
		}

		/// <summary>
		/// IsInt32 determines if the string value is a valid integer.
		/// </summary>
		/// <param name="data">A string value representing the integer to validate.</param>
		/// <returns>A boolean value representing the validity of the integer</returns>
		public static bool IsInt32(string data)
		{
			try
			{
				Int32.Parse(data);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// IsDateTime determines if the string value is a valid datetime.
		/// </summary>
		/// <param name="data">A string value representing the datetime to validate.</param>
		/// <returns>A boolean value representing the validity of the datetime.</returns>
		public static bool IsDateTime(string data)
		{
			try
			{
				DateTime.Parse(data);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// IsZeroOrOne determines if the string value is a "0" or "1" to most often denote a bit field.
		/// </summary>
		/// <param name="data">A string vallue representing the bit data to validate.</param>
		/// <returns>A boolean value representing whether the value was either a "0" or a "1".</returns>
		public static bool IsZeroOrOne(string data)
		{
			return ((data == "0") || (data == "1"));
		}

		/// <summary>
		/// IsDecimal determines if the string value is a valid decimal.
		/// </summary>
		/// <param name="data">A string value representing the decimal to validate.</param>
		/// <returns>A boolean value representing the validity of the decimal.</returns>
		public static bool IsDecimal(string data)
		{
			try
			{
				Decimal.Parse(data);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// IsCurrency determines if the string value is a valid currency.
		/// </summary>
		/// <param name="data">A string value representing the currency to validate.</param>
		/// <returns>A boolean value representing the validity of the currency.</returns>
		public static bool IsCurrency(string data)
		{
			try
			{
				Decimal.Parse(data, System.Globalization.NumberStyles.Currency);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
