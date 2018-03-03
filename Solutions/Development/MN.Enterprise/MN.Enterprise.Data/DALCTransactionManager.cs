using System.Collections;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// DALCTransactionManager manages <see cref="DALCTransaction"/>s when multiple transactions are necessary
    /// mostly when multiple data sources are being used within a single business workflow process.
    /// </summary>
    public class DALCTransactionManager
    {
        #region Constructor

        /// <summary>
        /// Default constructor to initialize the ArrayList that will contain a list of the <see cref="DALCTransaction"/> objects.
        /// </summary>
        public DALCTransactionManager()
        {
            _transactionList = new ArrayList();
        }

        #endregion

        #region Private Variables

        /// <summary>
        /// Represents the list of <see cref="DALCTransaction"/> this DALCTransactionManager is managing.
        /// </summary>
        private ArrayList _transactionList = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a <see cref="DALCTransaction"/> to the TransactionManager's transaction list.
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(DALCTransaction transaction)
        {
            _transactionList.Add(transaction);
        }

        /// <summary>
        /// Commits all <see cref="DALCTransaction"/>s in the TransactionManager's transaction list.
        /// </summary>
        public void CommitAllTransactions()
        {
            foreach (DALCTransaction txn in _transactionList)
            {
                txn.CommitDALCTransaction();
            }
        }

        /// <summary>
        /// Rolls back all <see cref="DALCTransaction"/>s in the TransactionManager's transaction list.
        /// </summary>
        public void RollbackAllTransactions()
        {
            foreach (DALCTransaction txn in _transactionList)
            {
                txn.RollbackDALCTransaction();
            }
        }

        #endregion
    }
}