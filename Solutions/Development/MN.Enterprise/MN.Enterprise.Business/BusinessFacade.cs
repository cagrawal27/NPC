using System;
using MN.Enterprise.Base;
using MN.Enterprise.Data;

namespace MN.Enterprise.Business
{
    /// <summary>
    /// The BusinessFacade class serves as the base class for .NET applications' business 
    /// interfaces allowing the application to perform a specific business task or tasks.
    /// Facade classes serve as controllers to the business workflow process, and also control
    /// business and database transaction management.  In general, these facade classes should 
    /// map to domain entities and provide a method representing a specific piece of the buiness 
    /// workflow that is related to that entity.  Additional business components may be accessed 
    /// during more complex buiness workflows and when business rules need to be implemented.  
    /// Buiness Facade classes serve as the middle-tier is accessing the data access layer in a 
    /// multi-tiered application.
    /// </summary>
    public class BusinessFacade
    {
        #region Private Variables

        /// <summary>
        /// DALCTransaction object for this instance of the BusinessFacade
        /// </summary>
        private DALCTransaction _transaction;

        /// <summary>
        /// DALCTransactionManager object for this instance of the BusinessFacade
        /// </summary>
        private DALCTransactionManager _transactionManager;

        /// <summary>
        /// Represents whether or not this instance of the BusinessFacade is constructed with the ability to partipate in transactions
        /// </summary>
        private bool _partipatesInTransaction;

        /// <summary>
        /// Represents whether or not this instance of the BusinessFacade is in a transaction
        /// </summary>
        private bool _isInTransaction;

        /// <summary>
        /// Represents whether or not this instance of the <see cref="DALCTransaction"/> has been aborted
        /// </summary>
        private bool _isTransactionAborted;

        /// <summary>
        /// Represents whether or not this <see cref="DALCTransaction"/> is being managed by a <see cref="DALCTransactionManager"/>
        /// </summary>
        private bool _hasTransactionManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to initialize the BusinessFacade's private varaiables, allowing the application developer
        /// to decide if this instance of the business facade should allow its participation in a <see cref="DALCTransaction"/>.
        /// </summary>
        /// <param name="businessFacadeBehavior"><see cref="BusinessFacadeBehavior"/> that defines the behavior for this instance
        /// of the business facade</param>
        public BusinessFacade(int businessFacadeBehavior)
        {
            // Initialize the participates in transactions appropriately depending on the input behavior parameter
            if (businessFacadeBehavior == BusinessFacadeBehavior.NONE)
                _partipatesInTransaction = false;
            else if (businessFacadeBehavior == BusinessFacadeBehavior.TRANSACTIONAL)
                _partipatesInTransaction = true;

            // Initialize variables
            _isInTransaction = false;
            _isTransactionAborted = false;
            _hasTransactionManager = false;
        }

        /// <summary>
        /// Constructor with a <see cref="DALCTransaction"/> to initialize the BusinessFacade's private variables.
        /// Called if this instance of the business facade is to be created within the scope of an existing <see cref="DALCTransaction"/>.
        /// </summary>
        /// <param name="transaction"><see cref="DALCTransaction"/> that has been previously created for a specific business workflow</param>
        public BusinessFacade(DALCTransaction transaction)
        {
            // Check input DALCTransaction
            if (transaction == null)
                throw new MNFatalException(
                    "Transaction parameter must not be null to perform data access logic using a transaction.");

            // Assign the transaction object
            _transaction = transaction;

            // Initialize variables
            _partipatesInTransaction = true;
            _isInTransaction = true;
            _isTransactionAborted = false;
            _hasTransactionManager = false;
        }

        /// <summary>
        /// Constructor with a <see cref="DALCTransactionManager"/> to initialize the BusinessFacade's private variables.
        /// Called if multiple <see cref="DALCTransaction"/> are being managed, most often in the case of having the need
        /// to work with multiple data sources, i.e. Sql Server and Oracle.
        /// </summary>
        /// <param name="transactionMgr"><see cref="DALCTransactionManager"/> that has been created to manage the multiple
        /// <see cref="DALCTransaction"/>'s that have been created to orchestrate the business workflow.</param>
        public BusinessFacade(DALCTransactionManager transactionMgr)
        {
            // Check input DALCTransactionManager
            if (transactionMgr == null)
                throw new MNFatalException(
                    "Transaction Manager parameter must not be null to perform data access logic using a transaction manager.");

            // Assign the transaction manager
            _transactionManager = transactionMgr;

            // Initialize variables
            _partipatesInTransaction = true;
            _isInTransaction = false;
            _isTransactionAborted = false;
            _hasTransactionManager = true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the <see cref="DALCTransaction"/> instantiated by this BusinessFacade
        /// </summary>
        /// <returns>The <see cref="DALCTransaction"/> created by the Start method of this instance of the BusinessFacade</returns>
        protected DALCTransaction GetTransaction()
        {
            // Return the DALCTransaction object for this instance of the BusinessFacade
            return _transaction;
        }

        /// <summary>
        /// Starts a <see cref="DALCTransaction"/> if this instance of the BusinessFacade was constructed to participate in 
        /// transactions.  It also sets the transaction's private variables appropriately and pushes the created Guid for the
        /// BusinessFacade method calling Start() to the transaction's stack of users.
        /// </summary>
        protected void Start()
        {
            // Only start a transaction if the business facade was constructed to partipate in transactions
            if (_partipatesInTransaction)
            {
                //Start a transaction if one does not exist, otherwise it uses the existing transaction
                if (!_isInTransaction)
                {
                    //Create the DALCTransaction and set it to the private variable
                    _transaction = new DALCTransaction(Guid.NewGuid());
                    _isInTransaction = true;

                    //Add the transaction to the TransactionManager if one exists
                    if (_hasTransactionManager)
                        _transactionManager.AddTransaction(_transaction);
                }
                else
                {
                    //Push a new GUID to the transaction users stack
                    _transaction.PushTransactionUser(Guid.NewGuid());
                }
            }
        }

        /// <summary>
        /// Votes to commit the current <see cref="DALCTransaction"/>.  If this transaction user is the transaction owner,
        /// the <see cref="DALCTransaction"/> will be commited.
        /// </summary>
        protected void SetComplete()
        {
            // Only mark the method as complete if the business facade was constructed to partipate in transactions
            if (_partipatesInTransaction)
            {
                //Get the current user off the transaction users stack
                Guid currentUser = _transaction.PopTransactionUser();

                //If the current user is not the owner or this transaction is being managed by a TransactionManager, 
                //return by casting a successful vote
                if (currentUser != _transaction.GetTransactionOwner() || _hasTransactionManager)
                    return;

                //Since this is the transaction owner, call the CommitDALCTransaction method to commit the transaction
                _transaction.CommitDALCTransaction();

                //Reset private variables
                _isInTransaction = false;
                _isTransactionAborted = false;
            }
        }

        /// <summary>
        /// Votes to abort the current <see cref="DALCTransaction"/>.  If this transaction user is the transaction owner,
        /// the <see cref="DALCTransaction"/> will be rolled back and aborted.
        /// </summary>
        protected void SetAbort()
        {
            // Only mark the method as failed if the business facade was constructed to partipate in transactions
            if (_partipatesInTransaction)
            {
                //If the transaction has already been aborted, no need to rollback
                if (_isTransactionAborted)
                    return;

                //Get the current user off the transaction users stack
                Guid currentUser = _transaction.PopTransactionUser();

                //If the current user is not the owner or this transaction is being managed by a TransactionManager, 
                //return by casting a unsuccessful vote
                if (currentUser != _transaction.GetTransactionOwner() || _hasTransactionManager)
                    return;

                //Reset private variables
                _isTransactionAborted = true;
                _transaction.RollbackDALCTransaction();
            }
        }

        #endregion
    }
}