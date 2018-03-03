namespace MN.Enterprise.Data
{
    /// <summary>
    /// The DALCUpdateBehavior enumeration definition is used to specify a set of constant integer values that 
    /// correspond to update behaviors to be used to define a <c cref="DALCTransaction"/>'s behavior transactional
    /// interference.
    /// </summary>
    public class DALCUpdateBehavior
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DALCUpdateBehavior()
        {
        }

        /// <summary>
        /// The STANDARD UpdateBehavior provides no interference with the DataAdapter's 
        /// update command.  If the update encounters an error, the update stops.  Additional rows 
        /// in the DataTable are uneffected.
        /// </summary>
        public const int STANDARD = 0;

        /// <summary>
        /// The CONTINUE UpdateBehavior provides interference with the DataAdapter's 
        /// update command.  If the update encounters an error, the update will continue.  The update 
        /// command will try to update the remaining rows.
        /// </summary>
        public const int CONTINUE = 1;

        /// <summary>
        /// The TRANSACTIONAL UpdateBehavior provides transactional interference with the 
        /// DataAdapter's update command.  If the update encounters an error, all updated rows will
        /// be rolled back.
        /// </summary>
        public const int TRANSACTIONAL = 2;
    }
}