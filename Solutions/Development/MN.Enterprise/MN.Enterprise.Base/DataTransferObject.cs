using System;
using System.Text;

namespace MN.Enterprise.Base
{
	/// <summary>
	/// The DataTransferObject class abstracts the structure in which data is to 
	/// be represented and a method in which may be passed between the distributed 
	/// layers of a .NET application.
	/// </summary>
	[Serializable]
	public abstract class DataTransferObject
	{
		/// <summary>
		/// Maintains state of the IsValid() method.
		/// </summary>
		private StringBuilder _ValidationMessage;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DataTransferObject()
		{
		}

		/// <summary>
		/// Indicates whether or not the data contained within the DataTransferObject
		/// is valid from a data type perspective and sets the ValidationMessage 
		/// property to indicate that overall validity.
		/// </summary>
		/// <returns>A boolean representing the validity of the data fields within the DataTransferObject</returns>
		/// <remarks>Does not have to overridden unless the user wants to provide datatype validation logic</remarks>
		public virtual bool IsValid()
		{
			return true;
		}

		/// <summary>
		/// Public property to access the ValidationMessage.
		/// </summary>
		public virtual string ValidationMessage
		{
			get
			{
				if (_ValidationMessage == null)
				{
					return null;
				}
				else
				{
					return _ValidationMessage.ToString();
				}
			}
		}

		/// <summary>
		/// Appends a new validation message to the public property.
		/// </summary>
		/// <param name="Message">String value to be added to the validation message</param>
		public void Append(string Message)
		{
			//Create a new StringBuilder if this is the first time appending to the ValidationMessage.
			if (_ValidationMessage == null)
			{
				_ValidationMessage = new StringBuilder();
			}

			//Add Message
			_ValidationMessage.Append(Message);
			_ValidationMessage.Append(Environment.NewLine);
		}
	}
}
