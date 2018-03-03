namespace MN.Enterprise.Data
{
    /// <summary>
    /// The DALCUpdateBehavior enumeration definition is used to specify a set of constant integer values that 
    /// correspond to isolation levels to be used to define a <c cref="DALCTransaction"/>'s locking behavior.
    /// </summary>
    public class DALCIsolationLevel
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DALCIsolationLevel()
        {
        }

        /// <summary>
        /// The CHAOS IsolationLevel designates that the pending changes from more 
        /// highly isolated transactions cannot be overwritten.
        /// </summary>
        public const int CHAOS = 16;

        /// <summary>
        /// The READ_COMMITTED IsolationLevel designates that shared locks are held while the 
        /// data is being read to avoid dirty reads, but the data can be changed before the 
        /// end of the transaction, resulting in non-repeatable reads or phantom data.
        /// </summary>
        public const int READ_COMMITTED = 4096;

        /// <summary>
        /// The READ_UNCOMMITTED IsolationLevel designates that a dirty read is possible, meaning 
        /// that no shared locks are issued and no exclusive locks are honored.
        /// </summary>
        public const int READ_UNCOMMITTED = 256;

        /// <summary>
        /// The REPEATABLE_READ IsolationLevel designates that locks are placed on all data that 
        /// is used in a query, preventing other users from updating the data. Prevents 
        /// non-repeatable reads but phantom rows are still possible.
        /// </summary>
        public const int REPEATABLE_READ = 65536;

        /// <summary>
        /// The SERIALIZABLE IsolationLevel designates that a range lock is placed on the DataSet, 
        /// preventing other users from updating or inserting rows into the dataset until the 
        /// transaction is complete.
        /// </summary>
        public const int SERIALIZABLE = 1048576;
    }
}