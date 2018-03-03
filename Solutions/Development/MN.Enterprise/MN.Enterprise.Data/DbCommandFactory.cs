using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// DbCommandFactory is the class to be used to create <see cref="DbCommandWrapper"/> objects
    /// </summary>
    public class DbCommandFactory
    {
        /// <summary>
        /// Creates a Stored Procedure <see cref="DbCommandWrapper"/> using the <see cref="Database"/> parameter
        /// </summary>
        /// <param name="database">The <see cref="Database"/> to create the <see cref="DbCommandWrapper"/> against.</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <returns>A <see cref="DbCommandWrapper"/> representing the command to be executed</returns>
        public static DbCommandWrapper GetStoredProcCommandWrapper(Database database, string storedProcedureName)
        {
            // validate that the database is not null
            if (database == null)
            {
                throw new MNFatalException("The Database parameter must not be null");
            }

            // create the command and the wrapper
            DbCommand command = database.GetStoredProcCommand(storedProcedureName);
            DbCommandWrapper cw = new DbCommandWrapper(database, command);
            return cw;
        }

        /// <summary>
        /// Creates a SQL String <see cref="DbCommandWrapper"/> using the <see cref="Database"/> parameter
        /// </summary>
        /// <param name="database">The <see cref="Database"/> to create the <see cref="DbCommandWrapper"/> against.</param>
        /// <param name="query">The SQL query to be executed</param>
        /// <returns>A <see cref="DbCommandWrapper"/> representing the command to be executed</returns>
        public static DbCommandWrapper GetSqlStringCommandWrapper(Database database, string query)
        {
            // validate that the database is not null
            if (database == null)
            {
                throw new MNFatalException("The Database parameter must not be null");
            }

            // create the command and the wrapper
            DbCommand command = database.GetSqlStringCommand(query);
            DbCommandWrapper cw = new DbCommandWrapper(database, command);
            return cw;
        }
    }
}