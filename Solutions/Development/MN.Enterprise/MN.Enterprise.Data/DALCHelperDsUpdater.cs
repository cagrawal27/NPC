using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// The DALCHelperDsUpdater class abstracts the common functionality to be used by
    /// specified Data Access Logic Components that provide the ability to update a database
    /// through the use of a DataSet.  This special <see cref="DALCHelper"/> class, in which 
    /// this class inherits, provides a consistent interface for data access logic components
    /// pertaining to the use of a DataSet for updating a database.
    /// </summary>
    public abstract class DALCHelperDsUpdater : DALCHelper
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DALCHelperDsUpdater()
        {
        }

        /// <summary>
        /// The implementation of this abstract method should return the <see cref="DALCUpdateBehavior"/>
        /// necessary to provides control over behavior if the data adapter's update command 
        /// encounters an error.
        /// </summary>
        /// <returns>The <see cref="DALCUpdateBehavior"/> enumeration value that corresponds to the 
        /// specified update behavior for this DALCHelperDsUpdater class.</returns>
        public abstract int GetDsUpdateBehavior();

        /// <summary>
        /// The implementation of this abstract method should return the name of the source
        /// table to use for table mapping in updating the database with the DataSet.
        /// </summary>
        /// <returns>The string value of the DataTable within the DataSet that is used for the update.</returns>
        public abstract string GetDsTableName();

        /// <summary>
        /// The implementation of this abstract method should initialize the DBCommandWrapper object
        /// for the Insert Command to reconcile changes to the database within the DataSet.
        /// </summary>
        /// <param name="db">The Microsoft.Practices.EnterpriseLibrary.Data object that represents an 
        /// abstract database that commands can be run against.</param>
        public abstract DbCommandWrapper InitializeInsertCommand(Database db);

        /// <summary>
        /// The implementation of this abstract method should initialize the cDBCommandWrapper object
        /// for the Update Command to reconcile changes to the database within the DataSet.
        /// </summary>
        /// <param name="db">The Microsoft.Practices.EnterpriseLibrary.Data object that represents an 
        /// abstract database that commands can be run against.</param>
        public abstract DbCommandWrapper InitializeUpdateCommand(Database db);

        /// <summary>
        /// The implementation of this abstract method should initialize the DBCommandWrapper object
        /// for the Delete Command to reconcile changes to the database within the DataSet.
        /// </summary>
        /// <param name="db">The Microsoft.Practices.EnterpriseLibrary.Data object that represents an 
        /// abstract database that commands can be run against.</param>
        public abstract DbCommandWrapper InitializeDeleteCommand(Database db);
    }
}