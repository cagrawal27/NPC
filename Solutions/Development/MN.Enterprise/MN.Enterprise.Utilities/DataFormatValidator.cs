using System;

namespace MN.Enterprise.Utilities
{
	/// <summary>
	/// DataFormatValidator is a class containing utility methods for determing if the given data
	/// conforms to a particular data format.
	/// </summary>
	public class DataFormatValidator
	{
		
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DataFormatValidator()
		{
		}

		#endregion


		#region Validates Social Security Number Format

		/// <summary>
		/// IsValidSSN validates an input string value of a social security number 
		/// accoding to SSA guidelines.
		/// </summary>
		/// <param name="ssn">A string value containing the 9-digit social security 
		/// number without dashes.</param>
		/// <returns>A boolean value; true if it is a valid social security number, 
		/// and false if it is an invalid social security number.</returns>
		public bool IsValidSSN(string ssn)
		{
			//Validate against same number and simple increments
			if(ssn=="123456789" || ssn=="000000000" || ssn=="111111111" || 
				ssn=="222222222" || ssn=="333333333" || ssn=="444444444" ||
				ssn=="555555555" || ssn=="666666666" || ssn=="777777777" ||
				ssn=="888888888" || ssn=="999999999" || ssn=="012345678")
				return false;
			
			//A valid area number is determined by either the state or zipcode; any number beginning 
			//with 000 will never be a valid ssn
			int firstThree = Convert.ToInt32(ssn.Substring(0, 3));
			if(firstThree == 0)
				return false;

			//A valid group number range is between 01 and 99
			int middleTwo = Convert.ToInt32(ssn.Substring(3, 2));
			if(middleTwo == 0)
				return false;

			//A valid serial number range is between 0001 and 9999
			int lastFour = Convert.ToInt32(ssn.Substring(5));
			if(lastFour == 0)
				return false;
			
			return true;
		}

		#endregion

	}
}
