namespace MN.Enterprise.Logging
{
    /// <summary>
    /// LogLevel is an enumeration of logging levels to abstract service-specific logging levels -
    /// concrete providers will need to map these accordingly.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Log debugging entry - the lowest level.
        /// </summary>
        DEBUG,

        /// <summary>
        /// Log information entry - the level above DEBUG.
        /// </summary>
        INFO,

        /// <summary>
        /// Log a warning - the level above INFO.
        /// </summary>
        WARN,

        /// <summary>
        /// Log an exception / error - the level above WARN.
        /// </summary>
        ERROR,

        /// <summary>
        /// Log an application-terminating event - the highest logging level.
        /// </summary>
        FATAL
    }

    /// <summary>
    /// Logger is the central class used for logging to files, event logs, databases,
    /// trace listeners, etc...  Log entries are categorized by the logging method chosen:
    /// DEBUG, INFO, WARN, ERROR, FATAL
    /// </summary>
    //public class Logger
    //{
    //    // interface used to log entries
    //    private ILog _log;

    //    #region Constructors

    //    /// <summary>
    //    /// Constructor to create a Logger named logger instance
    //    /// </summary>
    //    /// <param name="loggerName">Name for the logger being retrieved.  This will usually be
    //    /// the name of the calling class</param>
    //    private Logger(string loggerName)
    //    {
    //        // if the loggerName passed in is null or empty string, then use the name
    //        // of the logger class to create the logger.
    //        if (loggerName != null && loggerName.Trim().Length > 0)
    //        {
    //            _log = LogManager.GetLogger(loggerName);
    //        }
    //        else
    //        {
    //            _log = LogManager.GetLogger(this.GetType().ToString());
    //        }
    //    }

    //    #endregion


    //    #region Public Methods

    //    /// <summary>
    //    /// GetLogger returns a Logger named logger instance
    //    /// </summary>
    //    /// <param name="loggerName">Name of the logger to create</param>
    //    /// <returns>Named Logger instance</returns>
    //    public static Logger GetLogger(string loggerName)
    //    {
    //        return new Logger(loggerName);
    //    }

    //    #endregion


    //    #region Debug Log Methods

    //    /// <summary>
    //    /// Logs a debug message
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    public void Debug(string message)
    //    {
    //        Debug(message, null, null);
    //    }

    //    /// <summary>
    //    /// Logs a debug message and an <see cref="Exception" />
    //    /// </summary>
    //    /// <param name="message">The message text to be logged.</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    public void Debug(string message, Exception ex)
    //    {
    //        Debug(message, ex, null);
    //    }

    //    /// <summary>
    //    /// Logs a debug message and one or more serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="serializable">One or more serializable objects.</param>
    //    public void Debug(string message, params object[] serializable)
    //    {
    //        Debug(message, null, serializable);
    //    }

    //    /// <summary>
    //    /// Logs a debug message, an <see cref="Exception" />, and one or more
    //    /// serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    /// <param name="serializable">One ore more serializable objects</param>
    //    public void Debug(string message, Exception ex, params object[] serializable)
    //    {
    //        Log(message, ex, LogLevel.DEBUG, serializable);
    //    }

    //    #endregion


    //    #region Info Log Methods

    //    /// <summary>
    //    /// Logs a info message
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    public void Info(string message)
    //    {
    //        Info(message, null, null);
    //    }

    //    /// <summary>
    //    /// Logs a info message and an <see cref="Exception" />
    //    /// </summary>
    //    /// <param name="message">The message text to be logged.</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    public void Info(string message, Exception ex)
    //    {
    //        Info(message, ex, null);
    //    }

    //    /// <summary>
    //    /// Logs a info message and one or more serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="serializable">One or more serializable objects.</param>
    //    public void Info(string message, params object[] serializable)
    //    {
    //        Info(message, null, serializable);
    //    }

    //    /// <summary>
    //    /// Logs a info message, an <see cref="Exception" />, and one or more
    //    /// serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    /// <param name="serializable">One ore more serializable objects</param>
    //    public void Info(string message, Exception ex, params object[] serializable)
    //    {
    //        Log(message, ex, LogLevel.INFO, serializable);
    //    }

    //    #endregion


    //    #region Warn Log Methods

    //    /// <summary>
    //    /// Logs a warn message
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    public void Warn(string message)
    //    {
    //        Warn(message, null, null);
    //    }

    //    /// <summary>
    //    /// Logs a warn message and an <see cref="Exception" />
    //    /// </summary>
    //    /// <param name="message">The message text to be logged.</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    public void Warn(string message, Exception ex)
    //    {
    //        Warn(message, ex, null);
    //    }

    //    /// <summary>
    //    /// Logs a warn message and one or more serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="serializable">One or more serializable objects.</param>
    //    public void Warn(string message, params object[] serializable)
    //    {
    //        Warn(message, null, serializable);
    //    }

    //    /// <summary>
    //    /// Logs a warn message, an <see cref="Exception" />, and one or more
    //    /// serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    /// <param name="serializable">One ore more serializable objects</param>
    //    public void Warn(string message, Exception ex, params object[] serializable)
    //    {
    //        Log(message, ex, LogLevel.WARN, serializable);
    //    }

    //    #endregion


    //    #region Error Log Methods

    //    /// <summary>
    //    /// Logs a error message
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    public void Error(string message)
    //    {
    //        Error(message, null, null);
    //    }

    //    /// <summary>
    //    /// Logs a error message and an <see cref="Exception" />
    //    /// </summary>
    //    /// <param name="message">The message text to be logged.</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    public void Error(string message, Exception ex)
    //    {
    //        Error(message, ex, null);
    //    }

    //    /// <summary>
    //    /// Logs a error message and one or more serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="serializable">One or more serializable objects.</param>
    //    public void Error(string message, params object[] serializable)
    //    {
    //        Error(message, null, serializable);
    //    }

    //    /// <summary>
    //    /// Logs a error message, an <see cref="Exception" />, and one or more
    //    /// serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    /// <param name="serializable">One ore more serializable objects</param>
    //    public void Error(string message, Exception ex, params object[] serializable)
    //    {
    //        Log(message, ex, LogLevel.ERROR, serializable);
    //    }

    //    #endregion


    //    #region Fatal Log Methods

    //    /// <summary>
    //    /// Logs a fatal message
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    public void Fatal(string message)
    //    {
    //        Fatal(message, null, null);
    //    }

    //    /// <summary>
    //    /// Logs a fatal message and an <see cref="Exception" />
    //    /// </summary>
    //    /// <param name="message">The message text to be logged.</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    public void Fatal(string message, Exception ex)
    //    {
    //        Fatal(message, ex, null);
    //    }

    //    /// <summary>
    //    /// Logs a fatal message and one or more serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="serializable">One or more serializable objects.</param>
    //    public void Fatal(string message, params object[] serializable)
    //    {
    //        Fatal(message, null, serializable);
    //    }

    //    /// <summary>
    //    /// Logs a fatal message, an <see cref="Exception" />, and one or more
    //    /// serializable objects
    //    /// </summary>
    //    /// <param name="message">The message text to be logged</param>
    //    /// <param name="ex">The Exception to be logged</param>
    //    /// <param name="serializable">One ore more serializable objects</param>
    //    public void Fatal(string message, Exception ex, params object[] serializable)
    //    {
    //        Log(message, ex, LogLevel.FATAL, serializable);
    //    }

    //    #endregion


    //    #region Private Methods

    //    /// <summary>
    //    /// Central method for logging a log entry used by each of the level specific logging methods
    //    /// </summary>
    //    /// <param name="message">Text of message to be logged</param>
    //    /// <param name="ex">Exception to be logged if not null</param>
    //    /// <param name="logLevel">Level of log entry</param>
    //    /// <param name="serializable">One or more serializable objects to be added to message logged</param>
    //    public void Log(string message, Exception ex, LogLevel logLevel, params object[] serializable)
    //    {
    //        // build the message
    //        message = BuildMessage(message, serializable);

    //        // log the message based on LogLevel and if the chosen
    //        // level is enabled in log4net
    //        if (logLevel == LogLevel.DEBUG && _log.IsDebugEnabled)
    //        {
    //            _log.Debug(message, ex);
    //        }
    //        else if (logLevel == LogLevel.INFO && _log.IsInfoEnabled)
    //        {
    //            _log.Info(message, ex);
    //        }
    //        else if (logLevel == LogLevel.WARN && _log.IsWarnEnabled)
    //        {
    //            _log.Warn(message, ex);
    //        }
    //        else if (logLevel == LogLevel.ERROR && _log.IsErrorEnabled)
    //        {
    //            _log.Error(message, ex);
    //        }
    //        else if (logLevel == LogLevel.FATAL && _log.IsFatalEnabled)
    //        {
    //            _log.Fatal(message, ex);
    //        }
    //    }


    //    /// <summary>
    //    /// BuildMessage creates a message of information to log by serializing
    //    /// each object and concatenating them to the message string.
    //    /// </summary>
    //    /// <param name="message">A string of user-defined information to log.</param>
    //    /// <param name="objects">Objects to be serialzed and included in the logged message.</param>
    //    /// <returns>A string representing the complete message to log.</returns>
    //    private string BuildMessage(string message, object[] objects)
    //    {
    //        StringBuilder sb = new StringBuilder();

    //        if (message != null)
    //        {
    //            sb.Append(message + "\n");
    //        }


    //        // process the objects if there are any
    //        if (objects != null)
    //        {
    //            foreach (object o in objects)
    //            {
    //                if (o != null)
    //                {
    //                    sb.Append("------------------------------\n");
    //                    try
    //                    {
    //                        // Serialize the object and append to the
    //                        // message string
    //                        string xmlData = XmlHelper.Serialize(o);
    //                        sb.Append(XmlHelper.Serialize(o));
    //                        sb.Append("\n");
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        // add an error to the message b/c the object could not be serialized
    //                        sb.Append("Error while serializing object in Logger.BuildMessage().  Error: " + ex.Message + "\n");
    //                    }
    //                }
    //            }
    //        }
    //        sb.Append("\n");
    //        return sb.ToString();
    //    }

    //    #endregion
    //}
}