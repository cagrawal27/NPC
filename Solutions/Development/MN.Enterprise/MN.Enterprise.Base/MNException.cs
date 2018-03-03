using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Text;

namespace MN.Enterprise.Base
{
    /// <summary>
    /// The MNException class inherits from <see cref="ApplicationException"/> and is the custom
    /// class built for exception handling.  This class serves as the base 
    /// class for all other custom exception classes.
    /// </summary>
    public class MNException : ApplicationException
    {
        #region Private Variables

        /// <summary>
        /// Represents a collection of strings containing message trace data.
        /// </summary>
        private StringCollection _messageTraceData;

        /// <summary>
        /// <see cref="DataTransferObject"/> used as an input from the application code that 
        /// caused the exception.
        /// </summary>
        private DataTransferObject _dto;

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets the message trace data collection.
        /// </summary>
        public StringCollection MessageTraceData
        {
            get { return _messageTraceData; }
        }

        /// <summary>
        /// Gets and sets the <see cref="DataTransferObject"/>.
        /// </summary>
        public DataTransferObject DTO
        {
            get { return _dto; }
            set { _dto = value; }
        }

        /// <summary>
        /// Gets the exception message.  The ApplicationException base property 
        /// has been overridden to provide additional message trace data.
        /// </summary>
        public override string Message
        {
            get
            {
                StringBuilder msg = new StringBuilder(base.Message);

                if (null != _messageTraceData)
                {
                    foreach (String mess in _messageTraceData)
                    {
                        msg.Append(Environment.NewLine);
                        msg.Append(mess);
                    }
                }

                return msg.ToString();
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor; extends ApplicationException().
        /// </summary>
        public MNException()
            : base()
        {

        }

        /// <summary>
        /// Constructor that receives a <see cref="DataTransferObject"/> and 
        /// a <see cref="LogLevel"/>; extends ApplicationException().
        /// </summary>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the exception</param>
        public MNException(DataTransferObject dto)
            : base()
        {
            //Assign passed DataTransferObject to local DataTransferObject
            _dto = dto;
        }

        /// <summary>
        /// Constructor that receives a string message describing the exception and a 
        /// <see cref="LogLevel"/>; extends ApplicationException(string).
        /// </summary>
        /// <param name="message">The exception message</param>
        public MNException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor that receives a string message describing the exception 
        /// and a <see cref="DataTransferObject"/>; extends ApplicationException(string).
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the exception</param>
        public MNException(String message, DataTransferObject dto)
            : base(message)
        {
            //Assign passed DataTransferObject to local DataTransferObject
            _dto = dto;
        }

        /// <summary>
        /// Constructor that receives a string message describing the exception
        /// and the originating <see cref="Exception"/> to be used when replacing a non-MNException with
        /// a MNException; extends ApplicationException(string, Exception).
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The originating exception that was caught</param>
        public MNException(String message, Exception innerException)
            : base(message, innerException)
        {
            //Assign any message trace data
            AddMessageTraceData(innerException.Message);
        }

        /// <summary>
        /// Constructor that receives a string message describing the exception,  
        /// the originating exception, and a <see cref="DataTransferObject"/>.  This is used when replacing a 
        /// non-MNException with an MNException; extends ApplicationException(string, Exception).
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The exception that was caught</param>
        /// <param name="dto"><see cref="DataTransferObject"/> that is relevant to the 
        ///	application code causing the exception</param>
        public MNException(String message, Exception innerException, DataTransferObject dto)
            : base(message, innerException)
        {
            //Assign passed DataTransferObject to local DataTransferObject
            _dto = dto;

            //Assign any message trace data
            AddMessageTraceData(innerException.Message);
        }

        #endregion


        #region Public Methods
        /// <summary>
        /// Appends a message string to the trace data collection. MNException allows 
        /// "catchers" of the exception to add state data to the exception before 
        /// re-throwing it.
        /// </summary>
        /// <param name="message">The message to append</param>
        public void AddMessageTraceData(String message)
        {
            if (null == _messageTraceData)
                _messageTraceData = new StringCollection();

            _messageTraceData.Add(message);
        }

        #endregion


        #region Private Methods

        //private void DoLogEntry(string message, Exception innerException, LogLevel logLevel, DataTransferObject dto)
        //{
        //    Logger logger = Logger.GetLogger(this.GetType().FullName);

        //    // log the message based on LogLevel
        //    switch (logLevel)
        //    {
        //        case LogLevel.DEBUG:
        //            logger.Debug(message, innerException, dto);
        //            break;
        //        case LogLevel.INFO:
        //            logger.Info(message, innerException, dto);
        //            break;
        //        case LogLevel.WARN:
        //            logger.Warn(message, innerException, dto);
        //            break;
        //        case LogLevel.ERROR:
        //            logger.Error(message, innerException, dto);
        //            break;
        //        case LogLevel.FATAL:
        //            logger.Fatal(message, innerException, dto);
        //            break;
        //    }
        //}

        #endregion
    }
}

