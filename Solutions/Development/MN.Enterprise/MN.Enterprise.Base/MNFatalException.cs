using System;

namespace MN.Enterprise.Base
{
    /// <summary>
    /// The MNFatalException class inherits from <see cref="MNException"/> and is the custom
    /// class built for exception handling occuring from a unrecoverable platform and/or application failure.  
    /// </summary>
    public class MNFatalException : MNException
    {

        #region Constructors

        /// <summary>
        /// Default Constructor; extends MNException().
        /// </summary>
        public MNFatalException()
            : base()
        {
        }

        /// <summary>
        /// Constructor that receives a <see cref="DataTransferObject"/>; extends MNException(DataTransferObject).
        /// </summary>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the fatal exception</param>
        public MNFatalException(DataTransferObject dto)
            : base(dto)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the fatal exception; 
        /// extends MNException(string).
        /// </summary>
        /// <param name="message">The fatal exception message</param>
        public MNFatalException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the fatal exception, 
        /// and a <see cref="DataTransferObject"/>; extends MNException(string, DataTransferObject).
        /// </summary>
        /// <param name="message">The fatal exception message</param>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the fatal exception</param>
        public MNFatalException(String message, DataTransferObject dto)
            : base(message, dto)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the fatal exception, and
        /// the originating <see cref="Exception"/> to be used when replacing a non-MNException with
        /// a MNFatalException; extends MNException(string, Exception).
        /// </summary>
        /// <param name="message">The fatal exception message</param>
        /// <param name="innerException">The originating fatal exception that was caught</param>
        public MNFatalException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the fatal exception, the originating exception, 
        /// and a <see cref="DataTransferObject"/>.  This is used when replacing a non-MNException with a 
        /// MNFatalException; extends MNException(string, Exception, DataTransferObject).
        /// </summary>
        /// <param name="message">The fatal exception message</param>
        /// <param name="innerException">The fatal exception that was caught</param>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the fatal exception</param>
        public MNFatalException(String message, Exception innerException, DataTransferObject dto)
            : base(message, innerException, dto)
        {
        }

        #endregion

    }
}

