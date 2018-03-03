namespace MN.Enterprise.Business
{
    /// <summary>
    /// The BusinessFacadeBehavior enumeration definition is used to specify a set of constant integer values that 
    /// correspond to business facade behaviors as it relates to transaction support.
    /// </summary>
    public class BusinessFacadeBehavior
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BusinessFacadeBehavior()
        {
        }

        /// <summary>
        /// The NONE BusinessFacadeBehavior provides no transaction support within the 
        /// <see cref="BusinessFacade"/>.  A <see cref="OIT.Enterprise.Data.DALCTransaction"/> will not be started 
        /// and/or this business facade class will not partipate in a parent <see cref="OIT.Enterprise.Data.DALCTransaction"/>.
        /// </summary>
        public const int NONE = 0;

        /// <summary>
        /// The TRANSACTIONAL BusinessFacadeBehavior provides transaction support within the 
        /// <see cref="BusinessFacade"/>.  A <see cref="OIT.Enterprise.Data.DALCTransaction"/> will be started if not 
        /// already started given the Start() method and/or this business facade class will partipate 
        /// in a parent <see cref="OIT.Enterprise.Data.DALCTransaction"/>.
        /// </summary>
        public const int TRANSACTIONAL = 1;
    }
}