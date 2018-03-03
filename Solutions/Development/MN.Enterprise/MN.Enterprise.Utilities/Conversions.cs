using System;
using System.Collections;
using System.Text;

namespace MN.Enterprise.Utilities
{
	/// <summary>
	/// Conversions is a class containing utility methods for converting data from one format to another.
	/// </summary>
	public class Conversions
	{

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Conversions()
		{	
		}

		#endregion


		#region Converts a Dollar Amount to a Text Representation

		/// <summary>
		/// ConvertAmountToWords takes an input string dollar amount in a decimal format
		/// with an upper bound limit of 999,999.99 and converts it to a string denoting
		/// the text description of that dollar amount, i.e. One Hundred Dollars and 50/100.
		/// The purpose of this method is primarily for check writing needs in displaying
		/// the amount to the user.
		/// </summary>
		/// <param name="source">A string representing the currency amount in decimal format.</param>
		/// <returns>A string of text, representing the dollar amount on a check.</returns>
		public string ConvertAmountToWords(string source)
		{
			//Check for positive string rep
			if(source.IndexOf("-", 0, source.Length) != -1)
				return "Input string must be non negative.";

			//Check for multi decimals
			if(source.IndexOf(".", 0, source.Length) != source.Length - 3)
				return "Input string must contain only one decimal place.";

			//Check for non integers
			try
			{
				for(int j = 0; j < source.Length; j++)
				{
					if(source.Length-3 != j)
						Convert.ToInt16(source[j].ToString());
				}
			}
			catch
			{
				return "Input string must contain only numbers and a decimal point.";
			}

			//Local variable delcarations
			string dest = null;
			string[] ones = {"Zero"," One"," Two"," Three"," Four"," Five"," Six"," Seven"," Eight"," Nine"};
			string[] teens = {"Dummy"," Eleven"," Twelve"," Thirteen"," Fourteen"," Fifteen"," Sixteen"," Seventeen"," Eighteen"," Nineteen"};
			string[] tens = {"Dummy"," Ten"," Twenty"," Thirty"," Forty"," Fifty"," Sixty"," Seventy"," Eighty"," Ninety"};
			string hundred = " Hundred";
			string thousand = " Thousand";

			//Find and eval cents locale1
			int decIndex = source.IndexOf(".");

			if(decIndex == -1)
				decIndex = source.Length;

			//If source is too big
			if(decIndex > 6)
				return "Cannot process amounts greater than 999999.99";

			int i;

			//Everything before decimal point
			for(i = 0; i < decIndex; i++) 
			{
				//Conditional statements handles each digit in front of the decimal
				if(i == decIndex - 6)
				{
					dest += ones[Convert.ToInt32(source[i].ToString())];
					dest = dest.Replace(" ", "");
					dest += hundred;
				}
				else if(i == decIndex - 5)
				{
					if(source[i] != '0')
					{
						if(source[i] == '1' && source[i+1] != '0')
						{
							dest += teens[Convert.ToInt32(source[i+1].ToString())];
							dest += " Thousand";
							i++;
						}
						else
							dest += tens[Convert.ToInt32(source[i].ToString())];
					}
				}
				else if(i == decIndex - 4)
				{
					if(source[i] != '0')
						dest += ones[Convert.ToInt32(source[i].ToString())];

					dest += thousand;
				}
				else if(i == decIndex - 3)
				{
					dest += ones[Convert.ToInt32(source[i].ToString())];
					dest += hundred;
				}
				else if(i == decIndex - 2)
				{
					if(source[i] != '0')
					{
						if(source[i] == '1' && source[i+1] != 0)
						{
							dest += teens[Convert.ToInt32(source[i].ToString())];
							i++;
						}
						else
							dest += tens[Convert.ToInt32(source[i].ToString())];
					}
				}
				else if( i == decIndex - 1)
				{
					string s = ones[Convert.ToInt32(source[i].ToString())];

					if(s != "Zero" || (s == "Zero" && source.Length <= 4))
						dest += s;
				}
			}

			//Add and
			if(dest == "Zero")
				dest += " Dollars and";
			else
				dest += " and";

			//Increment past decimal point
			i++;

			//Concatonate everything past decimal point
			if(source.IndexOf(".") == -1)
				dest += " 00/100";
			else
				dest += " " + source.Substring(i,2) + "/100";
          
			return dest;
		}

		#endregion


		#region Convert an ArrayList to a CSV String

		/// <summary>
		/// ConvertArrayListToCSVString takes an input list of string values and converts
		/// that list into a single comma-separated string of the values.  The caller of
		/// this method may choose to include quotation marks or not for inclusion in the
		/// comma-separated string.
		/// </summary>
		/// <param name="list">An ArrayList of string values.</param>
		/// <param name="quotes">A boolean indicating whether to include single quotation 
		/// marks in the output comma-separated string.</param>
		/// <returns>A string of values separated by a comma and possibly including single
		/// quotation marks.</returns>
		public static string ConvertArrayListToCSVString(ArrayList list, bool quotes)
		{
			StringBuilder csv = new StringBuilder();
			bool first = true;
			
			foreach (String item in list)
			{    
				if(!first)
					csv.Append(",");
				else
					first = false;
			
				if(quotes)
					csv.Append("'" + item + "'");
				else
					csv.Append(item);
			}

			return csv.ToString();
		}

		#endregion

	}
}
