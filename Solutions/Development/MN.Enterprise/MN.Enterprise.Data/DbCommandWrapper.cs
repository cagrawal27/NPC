using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// DbCommandWrapper is the class used to wrap the <see cref="Database" /> and 
    /// <see cref="DbCommand" /> objects that are necessary for interacting with a back-end
    /// database.  DbCommandWrapper provides methods for managing parameters to be associated
    /// with a stored procedure or parameterized sql statement.
    /// </summary>
    public class DbCommandWrapper : IDisposable
    {
        /// <summary>
        /// Delegate to be used for anonymous method delegate inference
        /// </summary>
        /// <typeparam name="T">type to be used as the return Nullable type</typeparam>
        /// <param name="convertObject">object to convert</param>
        /// <returns></returns>
        private delegate T Conversion<T>(object convertObject);

        /// <summary>
        /// <see cref="Database" /> object used for connecting to the database
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// <see cref="DbCommand" /> object used for executing commands against the database
        /// </summary>
        private DbCommand _command = null;

        /// <summary>
        /// Integer used to track how many rows are affected by an executed command
        /// </summary>
        private int _rowsAffected = 0;

        #region Constructors

        /// <summary>
        /// Constructor containing the <see cref="Database" /> and <see cref="DbCommand" /> objects
        /// that are necessary for executing commands against a database.
        /// </summary>
        /// <param name="database"><see cref="Database" /> object representing the back-end database for the 
        ///     command to be executed against.</param>
        /// <param name="command"><see cref="DbCommand" /> object representing the individual command to be
        ///     executed against the database.</param>
        internal DbCommandWrapper(Database database, DbCommand command)
        {
            _database = database;
            _command = command;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Override method for IDisposable interface
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>Avalue indicating whether the parameter accepts null values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>
        public void AddParameter(string name, DbType dbType, int size, ParameterDirection direction, bool nullable,
                                 byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion,
                                 object value)
        {
            _database.AddParameter(_command, name, dbType, size, direction, nullable, precision, scale, sourceColumn,
                                   sourceVersion, value);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>
        public void AddParameter(string name, DbType dbType, ParameterDirection direction, string sourceColumn,
                                 DataRowVersion sourceVersion, object value)
        {
            _database.AddParameter(_command, name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        public void AddInParameter(string name, DbType dbType)
        {
            _database.AddInParameter(_command, name, dbType);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>
        public void AddInParameter(string name, DbType dbType, object value)
        {
            _database.AddInParameter(_command, name, dbType, value);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        public void AddInParameter(string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            _database.AddInParameter(_command, name, dbType, sourceColumn, sourceVersion);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        public void AddOutParameter(string name, DbType dbType, int size)
        {
            _database.AddOutParameter(_command, name, dbType, size);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as an object.</returns>
        public object GetParameterValue(string name)
        {
            return _database.GetParameterValue(_command, name);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable short.</returns>
        public Nullable<short> GetNullableInt16ParameterValue(string name)
        {
            return GetNullableParameterValue<short>(name, Convert.ToInt16);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable int.</returns>
        public Nullable<int> GetNullableInt32ParameterValue(string name)
        {
            return GetNullableParameterValue<int>(name, Convert.ToInt32);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable long.</returns>
        public Nullable<long> GetNullableInt64ParameterValue(string name)
        {
            return GetNullableParameterValue<long>(name, Convert.ToInt64);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable bool.</returns>
        public Nullable<bool> GetNullableBooleanParameterValue(string name)
        {
            return GetNullableParameterValue<bool>(name, Convert.ToBoolean);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable DateTime.</returns>
        public Nullable<DateTime> GetNullableDateTimeParameterValue(string name)
        {
            return GetNullableParameterValue<DateTime>(name, Convert.ToDateTime);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable decimal.</returns>
        public Nullable<decimal> GetNullableDecimalParameterValue(string name)
        {
            return GetNullableParameterValue<decimal>(name, Convert.ToDecimal);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a Nullable double.</returns>
        public Nullable<double> GetNullableDoubleParameterValue(string name)
        {
            return GetNullableParameterValue<double>(name, Convert.ToDouble);
        }

        /// <summary>
        /// Gets a parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as a string.</returns>
        public string GetStringParameterValue(string name)
        {
            return Convert.ToString(GetParameterValue(name));
        }

        /// <summary>
        /// Sets a parameter value.
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        public void SetParameterValue(string parameterName, object value)
        {
            _database.SetParameterValue(_command, parameterName, value);
        }

        #endregion

        #region Public Properties

        public int RowsAffected
        {
            get { return _rowsAffected; }
            set { _rowsAffected = value; }
        }

        #endregion

        #region Internal Properties

        internal DbCommand Command
        {
            get { return _command; }
        }

        internal Database Database
        {
            get { return _database; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This generic method will called each of the "Nullable"
        /// GetParameterValue methods.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Column name.</param>
        /// <param name="convert">Delegate to invoke if the value if the return value is not null</param>
        /// <returns></returns>
        private Nullable<T> GetNullableParameterValue<T>(string name, Conversion<T> convert) where T : struct
        {
            Nullable<T> nullable;

            object paramValue = GetParameterValue(name);

            if (paramValue == null)
            {
                nullable = null;
            }
            else
            {
                nullable = convert(paramValue);
            }
            return nullable;
        }

        #endregion
    }
}