using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// The DALCHelper class abstracts the common functionality across all Data
    /// Access Logic Components to provide a consistent interface for all 
    /// <see cref="DALCHelper"/>s and classes.
    /// </summary>
    public abstract class DALCHelper
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DALCHelper()
        {
        }

        /// <summary>
        /// The implementation of this virtual method should evaluate the criteria contained 
        /// within the input <see cref="DataTransferObject"/>, if required.  The default is
        /// true.
        /// </summary>
        /// <param name="criteria">The input <see cref="DataTransferObject"/> defining the criteria 
        /// to be used within the database procedure.</param>
        /// <returns>A boolean value denoting the the success or failure of the validation routine.</returns>
        public virtual bool CriteriaIsValid(DataTransferObject criteria)
        {
            return true;
        }

        /// <summary>
        /// The implementation of this abstract method should initialize the DBCommandWrapper
        /// with the information included in the database and the <see cref="DataTransferObject"/>,
        /// (creates a DBCommandWrapper for a stored procedure, dynamic sql text, etc.).
        /// It is expected that when this method returns the DBCommandWrapper is ready to 
        /// be properly executed.  This means the stored procedure name has been set and all of the
        /// necessary input and/or output parameters have been set.
        /// </summary>
        /// <param name="db">The <see cref="Microsoft.Practices.EnterpriseLibrary.Data.Database"/> object that represents an 
        /// abstract database that commands can be run against.</param>
        /// <param name="criteria">A <see cref="DataTransferObject"/> containing the values to be used in
        /// creating the input and output parameters associated with the database command.</param>
        /// <returns>The DBCommandWrapper ready to execute.</returns>
        public abstract DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria);

        /// <summary>
        /// The InitializeDsTableNames method should be overriden to initialize the string array
        /// with the table name(s) to be used when the LoadDataSet method is called.
        /// If the LoadDataSet method will not be used, this method may be ignored, since the base
        /// shall be returning a null string value.
        /// </summary>
        /// <returns>A string array of table names to be used by the LoadDataSet method.</returns>
        public virtual string[] InitializeDsTableNames()
        {
            return null;
        }

        /// <summary>
        /// The DbInstanceName method should be overriden to provide the configuration key for 
        /// database service when a specifc database service object is required.
        /// This value should be the instance name as defined in the data configuration
        /// file. If there is just one database instance defined, then this method
        /// does not need to be overridden; the default instance will be used, since the base
        /// shall be returning a null string value.
        /// </summary>
        /// <returns>A string value of the database instance name.</returns>
        public virtual string DbInstanceName()
        {
            return null;
        }

        /// <summary>
        /// The ConvertResultsDto method should be overriden to convert
        /// the rows within the IDataReader into a <see cref="DataTransferObject"/> to
        /// be returned to the calling business workflow method.  If the implementation
        /// of this method is not needed, then this method does not need to be overriden.
        /// The base will return a null value.
        /// </summary>
        /// <param name="cw">The DbCommand that was executed.</param>
        /// <param name="reader">The IDataReader containing the database results from the executed Sql query.</param>
        /// <returns>The <see cref="DataTransferObject"/> to return to the business services layer.</returns>
        public virtual DataTransferObject ConvertResultsDto(DbCommandWrapper cw, IDataReader reader)
        {
            return null;
        }

        /// <summary>
        /// The CreateResultsDto method should be overriden to perform activities to create
        /// the <see cref="DataTransferObject"/> containing output parameter information for non-query 
        /// database activities.  If the implementation of this method is not needed, 
        /// then this method does not need to be overriden. The base will return a 
        /// null value.
        /// </summary>
        /// <param name="cw">The DbCommand that was executed.</param>
        /// <param name="criteria">The input <see cref="DataTransferObject"/> containing the information that 
        /// was inserted into the database.</param>
        /// <returns>A <see cref="DataTransferObject"/> to return to the business services layer containing the 
        /// original information plus the relevant key information from the database.</returns>
        public virtual DataTransferObject CreateResultsDto(DbCommandWrapper cw, DataTransferObject criteria)
        {
            return null;
        }
    }

    public abstract class DALCHelper<T> : DALCHelper
        where T : DataTransferObject
    {
        public virtual List<T> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {
            return null;
        }
    }
}