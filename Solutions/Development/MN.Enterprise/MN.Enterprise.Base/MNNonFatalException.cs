using System;

namespace MN.Enterprise.Base
{
    /// <summary>
    /// The MNNonFatalException class inherits from <see cref="MNException"/> and is the custom
    /// class built for exception handling occuring from a recoverable platform and/or application failure.  
    /// </summary>
    public class MNNonFatalException : MNException
    {

        #region Constructors

        /// <summary>
        /// Default Constructor; extends MNException().
        /// </summary>
        public MNNonFatalException()
            : base()
        {
        }

        /// <summary>
        /// Constructor that receives a <see cref="DataTransferObject"/>; extends MNException(DataTransferObject).
        /// </summary>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the nonfatal exception</param>
        public MNNonFatalException(DataTransferObject dto)
            : base(dto)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the nonfatal exception; 
        /// extends MNException(string).
        /// </summary>
        /// <param name="message">The nonfatal exception message</param>
        public MNNonFatalException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the nonfatal exception, 
        /// and a <see cref="DataTransferObject"/>; extends MNException(string, DataTransferObject).
        /// </summary>
        /// <param name="message">The nonfatal exception message</param>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the nonfatal exception</param>
        public MNNonFatalException(String message, DataTransferObject dto)
            : base(message, dto)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the nonfatal exception, and
        /// the originating <see cref="Exception"/> to be used when replacing a non-MNException with
        /// a MNNonFatalException; extends MNException(string, Exception).
        /// </summary>
        /// <param name="message">The fatal exception message</param>
        /// <param name="innerException">The originating fatal exception that was caught</param>
        public MNNonFatalException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the nonfatal exception, the originating exception, 
        /// and a <see cref="DataTransferObject"/>.  This is used when replacing a non-MNException with a 
        /// MNNonFatalException; extends MNException(string, Exception, DataTransferObject).
        /// </summary>
        /// <param name="message">The nonfatal exception message</param>
        /// <param name="innerException">The nonfatal exception that was caught</param>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the nonfatal exception</param>
        public MNNonFatalException(String message, Exception innerException, DataTransferObject dto)
            : base(message, innerException, dto)
        {
        }

        #endregion

    }
}

