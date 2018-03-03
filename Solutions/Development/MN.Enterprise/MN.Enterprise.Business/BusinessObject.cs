using System;
using System.Text;

namespace MN.Enterprise.Business
{
    /// <summary>
    /// The BusinessObject class serves as the base class for .NET applications' business
    /// objects allowing the application to validate business rules given the class.
    /// Business objects implement business rules in diverse patterns and accet and return 
    /// simple or complex data structures.  These business objects should expose functionality
    /// in a way that is agnostic to the data stores and services needed to perform the work,
    /// and should be composed in meaningful and transactionally consistent ways.
    /// </summary>
    public class BusinessObject
    {
        #region Private Variables

        /// <summary>
        /// StringBuilder object maintaining the validation message.
        /// </summary>
        private StringBuilder _validationMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Default contructor.
        /// </summary>
        public BusinessObject()
        {
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Public property containing message information.
        /// </summary>
        public virtual string ValidationMessage
        {
            get
            {
                if (_validationMessage == null)
                {
                    return null;
                }
                else
                {
                    return _validationMessage.ToString();
                }
            }
        }

        #endregion

        #region Protected Method

        /// <summary>
        /// Appends a new string to the ValidationMessage.
        /// </summary>
        /// <param name="message"></param>
        protected void Append(string message)
        {
            //check if first time
            if (_validationMessage == null)
            {
                _validationMessage = new StringBuilder();
            }

            //add data
            _validationMessage.Append(message);
            _validationMessage.Append(Environment.NewLine);
        }

        #endregion
    }
}