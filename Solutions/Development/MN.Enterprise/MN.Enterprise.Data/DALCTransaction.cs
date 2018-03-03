using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// The DALCTransaction class wraps an IDbTransaction and the associated IDbConnection.
    /// The transaction is implicitly started when the class is instantiated, and 
    /// can be constructed with an <see cref="DALCIsolationLevel"/> for a specified locking behavior.
    /// This class also provides accesser, commit and rollback methods for the 
    /// contained IDbTransaction and IDbConnection objects.
    /// </summary>
    public class DALCTransaction
    {
        #region Private Variables

        /// <summary>
        /// Connection for the current DALCTransaction
        /// </summary>
        private DbConnection _connection;

        /// <summary>
        /// DbTransaction this DALCTransaction is wrapping
        /// </summary>
        private DbTransaction _transaction;

        /// <summary>
        /// Represents the DALCTransaction's owner, which is the first to start the DALCTransaction
        /// </summary>
        private Guid _transactionOwner;

        /// <summary>
        /// Contains a list of the DALCTransaction's users
        /// </summary>
        private Stack _transactionUsers;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor to get the database connection and begin the <see cref="DALCTransaction"/>.
        /// </summary>
        /// <param name="ownerGuid">GUID of the method that instantiated this <see cref="DALCTransaction"/></param>
        public DALCTransaction(Guid ownerGuid)
        {
            // Create the database and connection object to get the DALCTransaction
            GetDALCConnection();

            // Assign
            _transaction = _connection.BeginTransaction();

            // store the transaction owner GUID and create the transaction users
            // stack.  The owner GUID is the first GUID to be added to the stack.
            _transactionOwner = ownerGuid;
            _transactionUsers = new Stack();
            PushTransactionUser(ownerGuid);
        }

        /// <summary>
        /// Constructor to get the database connection and begin the <see cref="DALCTransaction"/> with
        /// a <see cref="DALCIsolationLevel"/> for the transaction locking behavior for the connection
        /// wrapped by the <see cref="DALCTransaction"/>.
        /// </summary>
        /// <param name="ownerGuid">GUID of the method that initiated this <see cref="DALCTransaction"/></param>
        /// <param name="isolationLevel">The <see cref="DALCIsolationLevel"/> which specifies the locking behavior 
        /// for the <see cref="DALCTransaction"/>.</param>
        public DALCTransaction(Guid ownerGuid, int isolationLevel)
        {
            GetDALCConnection();
            _transaction = _connection.BeginTransaction(GetIsolationLevel(isolationLevel));

            // store the transaction owner GUID and create the transaction users
            // stack.  The owner GUID is the first GUID to be added to the stack.
            _transactionOwner = ownerGuid;
            _transactionUsers = new Stack();
            PushTransactionUser(ownerGuid);
        }

        #endregion

        #region Accessor Methods

        /// <summary>
        /// Gets the DbTransaction object that the <see cref="DALCTransaction"/> is wrapping.
        /// </summary>
        /// <returns>DbTransaction</returns>
        public DbTransaction GetTransaction()
        {
            return _transaction;
        }

        /// <summary>
        /// Gets the transaction owner GUID that initiated this <see cref="DALCTransaction"/>.
        /// </summary>
        /// <returns>Guid</returns>
        public Guid GetTransactionOwner()
        {
            return _transactionOwner;
        }

        /// <summary>
        /// Gets the current DbConnection object that <see cref="DALCTransaction"/> is using.
        /// </summary>
        /// <returns>DbConnection</returns>
        public DbConnection GetConnection()
        {
            return _connection;
        }

        /// <summary>
        /// Gets the System.Data.IsolationLevel for the <see cref="DALCTransaction"/> if specified.
        /// </summary>
        /// <param name="isolationLevel">The <see cref="DALCIsolationLevel"/> which specifies the locking behavior 
        /// for the <see cref="DALCTransaction"/>.</param>
        /// <returns>System.Data.IsolationLevel</returns>
        private static IsolationLevel GetIsolationLevel(int isolationLevel)
        {
            switch (isolationLevel)
            {
                case DALCIsolationLevel.CHAOS:
                    return IsolationLevel.Chaos;
                case DALCIsolationLevel.READ_COMMITTED:
                    return IsolationLevel.ReadCommitted;
                case DALCIsolationLevel.READ_UNCOMMITTED:
                    return IsolationLevel.ReadUncommitted;
                case DALCIsolationLevel.REPEATABLE_READ:
                    return IsolationLevel.RepeatableRead;
                case DALCIsolationLevel.SERIALIZABLE:
                    return IsolationLevel.Serializable;
            }

            //return unspecified
            return IsolationLevel.Unspecified;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Commits the running IDbTransaction and then terminates the IDbTransaction.
        /// </summary>
        public void CommitDALCTransaction()
        {
            if (_transaction != null)
                _transaction.Commit();

            _transaction = null;
        }

        /// <summary>
        /// Rolls back the running IDbTransaction and then terminates the IDbTransaction.
        /// </summary>
        public void RollbackDALCTransaction()
        {
            if (_transaction != null)
                _transaction.Rollback();

            _transaction = null;
        }

        /// <summary>
        /// Pushes a new GUID to the top of the <see cref="DALCTransaction"/>'s transaction users stack.
        /// </summary>
        /// <param name="userGuid">GUID of the method</param>
        public void PushTransactionUser(Guid userGuid)
        {
            _transactionUsers.Push(userGuid);
        }

        /// <summary>
        /// Removes and returns the GUID at the top of the <see cref="DALCTransaction"/>'s transaction users stack.
        /// </summary>
        /// <returns>GUID of the method</returns>
        public Guid PopTransactionUser()
        {
            try
            {
                return (Guid) _transactionUsers.Pop();
            }
            catch (InvalidOperationException ioe)
            {
                throw new MNException("The transaction users stack is empty", ioe);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a database and establishes the connection
        /// </summary>
        private void GetDALCConnection()
        {
            Database db = DatabaseFactory.CreateDatabase();
            _connection = db.CreateConnection();
            _connection.Open();
        }

        #endregion
    }
}