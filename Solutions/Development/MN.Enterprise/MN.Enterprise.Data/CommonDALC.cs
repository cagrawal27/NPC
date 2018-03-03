using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// CommonDALC is the base class for all data access logic components to expose methods
    /// for inserting, deleting, updating, and retrieving data from a database.  Data access
    /// logic components shall provide access to database functionality, returning both simple
    /// and complex data structures.
    /// </summary>
    public abstract class CommonDALC
    {
        #region Private Variables

        /// <summary>
        /// DALCTransaction object wrapping the IDbTransaction and the IDbConnection to be used by this DALC class
        /// </summary>
        private DALCTransaction _transaction;

        /// <summary>
        /// Represents whether or not this instance of the CommonDALC is to use database transactioning
        /// </summary>
        private bool _isInTransaction;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected CommonDALC()
        {
            // Set DALCTransaction to null
            _transaction = null;

            // Set in transaction flag
            _isInTransaction = false;
        }

        /// <summary>
        /// Constructor containing a <see cref="DALCTransaction"/> for data access logic component(s) that 
        /// need to participate in a transaction. 
        /// </summary>
        /// <param name="transaction">The <see cref="DALCTransaction"/> to be used for this data operation.</param>
        protected CommonDALC(DALCTransaction transaction)
        {
            // Assign the DALCTransaction object
            _transaction = transaction;

            // Set in transaction flag.  The IDbTransaction may be null if the BusinessFacade was 
            // instantiated to not do transactioning.  A DALCTransaction is still created, just 
            // without the inner IDbTransaction.
            if (transaction == null || transaction.GetTransaction() == null)
            {
                _isInTransaction = false;
            }
            else
            {
                _isInTransaction = true;
            }
        }

        #endregion

        #region Execute Query Methods

        /// <summary>
        /// This method executes a query against the database and returns the results as a 
        /// <see cref="DataTransferObject"/>. The primary purpose of this method is to retrieve 
        /// a single entity (record) from the database (master) that may have associated child 
        /// records (details).
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper"/> class that contains the database 
        /// command information.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the criteria 
        /// to be used to determine mathing records in the database.</param>
        /// <returns>The <see cref="DataTransferObject"/> now populated with the record found
        /// in the database matching the specified criteria.</returns>
        protected DataTransferObject ExecuteQueryDto(DALCHelper helper, DataTransferObject criteria)
        {
            // Initialize objects
            IDataReader reader = null;
            Database db = null;
            DbCommandWrapper cw = null;

            // Initialize output parameters				
            DataTransferObject dto = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryDto was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryDto was passed invalid search criteria.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Execute a data reader
                if (_isInTransaction)
                    reader = db.ExecuteReader(command, _transaction.GetTransaction());
                else
                    reader = db.ExecuteReader(command);

                // set command for garbage collection
                command = null;

                // Convert the results into a data transfer object
                dto = helper.ConvertResultsDto(cw, reader);
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.ExecuteQueryDto");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.ExecuteQueryDto");
                throw ex;
            }
            finally
            {
                // Close the data reader if it has not already been closed
                if ((null != reader) && (reader.IsClosed == false))
                {
                    reader.Close();
                }

                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }

            // Return the data transfer object
            return dto;
        }


        /// <summary>
        /// This method executes a query against the database and returns the results
        /// as a IDataReader. The primary purpose of this method is to retrieve a set of matching
        /// records from the database that require data processing from the calling procedure.
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper"/> class that contains the database 
        /// command information.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the criteria 
        /// to be used to determine mathing records in the database.</param>
        /// <returns>The DataReader filled with the matching records found in the database matching 
        /// the specified criteria.</returns>
        protected IDataReader ExecuteQueryReader(DALCHelper helper, DataTransferObject criteria)
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper cw = null;

            // Initialize output parameters				
            IDataReader reader = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryReader was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryReader was passed invalid search criteria.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Execute a data reader
                if (_isInTransaction)
                    reader = db.ExecuteReader(command, _transaction.GetTransaction());
                else
                    reader = db.ExecuteReader(command);

                // set command for garbage collection
                command = null;
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.ExecuteQueryReader");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.ExecuteQueryReader");
                throw ex;
            }
            finally
            {
                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }

            // Return the data reader
            return reader;
        }


        /// <summary>
        /// This method executes a query against the database and returns the results
        /// as a DataSet. The primary purpose of this method is to retrieve a set of matching
        /// records from the database that require data processing from the calling procedure.
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper"/> class that contains the database 
        /// command information.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the criteria 
        /// to be used to determine mathing records in the database.</param>
        /// <returns>The DataSet filled with the matching records found in the database matching 
        /// the specified criteria.</returns>
        protected DataSet ExecuteQueryDs(DALCHelper helper, DataTransferObject criteria)
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper cw = null;

            // Initialize output parameters				
            DataSet ds = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryDs was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryDs was passed invalid search criteria.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Execute a dataset
                if (_isInTransaction)
                    ds = db.ExecuteDataSet(command, _transaction.GetTransaction());
                else
                    ds = db.ExecuteDataSet(command);

                // set command for garbage collection
                command = null;
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.ExecuteQueryDs");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.ExecuteQueryDs");
                throw ex;
            }
            finally
            {
                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }

            // Return the dataset
            return ds;
        }


        /// <summary>
        /// This method executes a query against the database and returns the results
        /// as a DataSet. The primary purpose of this method is to retrieve a set of matching
        /// records from the database that require data processing from the calling procedure.
        /// The difference between ExecuteQueryDs and LoadDataSet is that this method allows 
        /// an existing DataSet to be passed in as an input parameter such that additional 
        /// results may be added to it versus a new DataSet being created.
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper"/> class that contains the database 
        /// command information.</param>
        /// <param name="ds">An existing DataSet to which  more information shall be added.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the criteria 
        /// to be used to determine mathing records in the database.</param>
        /// <returns>The DataSet filled with the matching records found in the database matching 
        /// the specified criteria.</returns>
        protected void LoadDataSet(DALCHelper helper, DataSet ds, DataTransferObject criteria)
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper cw = null;
            string[] tableNames = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.LoadDataSet was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.LoadDataSet was passed invalid search criteria.");

            // make sure the InitializeDSTableNames method is not returning
            // null.
            tableNames = helper.InitializeDsTableNames();
            if (tableNames == null)
                throw new MNNonFatalException("CommonDALC.LoadDataSet has no table names specified.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Load the dataset
                if (_isInTransaction)
                    db.LoadDataSet(command, ds, tableNames, _transaction.GetTransaction());
                else
                    db.LoadDataSet(command, ds, tableNames);

                // set command for garbage collection
                command = null;
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.LoadDataSet");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.LoadDataSet");
                throw ex;
            }
            finally
            {
                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }
        }


        /// <summary>
        /// This method executes a query against the database and returns the results
        /// as a List. The primary purpose of this method is to retrieve a set of matching
        /// records from the database that require data processing from the calling procedure.
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper<T>"/> class that contains the database 
        /// command information.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the criteria 
        /// to be used to determine mathing records in the database.</param>
        /// <returns>The ArrayList filled with the matching records in the format specified by the
        /// <see cref="DALCHelper"/>, i.e. a List of <see cref="DataTransferObject"/>s.</returns>
        protected List<T> ExecuteQueryList<T>(DALCHelper<T> helper, DataTransferObject criteria)
            where T : DataTransferObject
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper cw = null;
            List<T> returnList = null;

            // Initialize output parameters				
            IDataReader reader = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryList was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.ExecuteQueryList was passed invalid search criteria.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Execute a data reader
                if (_isInTransaction)
                    reader = db.ExecuteReader(command, _transaction.GetTransaction());
                else
                    reader = db.ExecuteReader(command);

                // convert the results to an ArrayList using the helper instance
                returnList = helper.ConvertResultsList(cw, reader);

                // set command for garbage collection
                command = null;
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.ExecuteQueryList");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.ExecuteQueryList");
                throw ex;
            }
            finally
            {
                // Close the data reader if it has not already been closed
                if ((null != reader) && (reader.IsClosed == false))
                {
                    reader.Close();
                }

                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }

            // Return the converted results list
            return returnList;
        }

        #endregion

        #region Execute Non-Query Methods

        /// <summary>
        /// This method executes a non-query against the database. The primary purpose of this method 
        /// is to insert, update or delete data in the database.
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper"/> class that contains the database 
        /// command information.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the data to be
        /// used for the insert or update and/or the criteria used to determine mathing records in 
        /// the database to be updated.</param>
        /// <returns>A long integer representing the count of records affected</returns>
        protected int ExecuteNonQuery(DALCHelper helper, DataTransferObject criteria)
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper cw = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.ExecuteNonQuery was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.ExecuteNonQuery was passed invalid data and/or criteria.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Execute the command
                int recordsAffected = 0;
                if (_isInTransaction)
                    recordsAffected = db.ExecuteNonQuery(command, _transaction.GetTransaction());
                else
                    recordsAffected = db.ExecuteNonQuery(command);

                // set command for garbage collection
                command = null;

                // return the count
                return recordsAffected;
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.ExecuteNonQuery");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.ExecuteNonQuery");
                throw ex;
            }
            finally
            {
                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }
        }


        /// <summary>
        /// This method executes a non-query against the database. The primary purpose of this method 
        /// is to insert data in the database and have a <see cref="DataTransferObject"/> be returned 
        /// containing the key set for a record created in the database.
        /// </summary>
        /// <param name="helper">The <see cref="DALCHelper"/> class that contains the database 
        /// command information.</param>
        /// <param name="criteria">The <see cref="DataTransferObject"/> containing the data to be
        /// used for the insert or update and/or the criteria used to determine mathing records in 
        /// the database to be updated.</param>
        /// <returns>The <see cref="DataTransferObject"/> containing key set data for the created/inserted
        /// record in the database.</returns>
        protected DataTransferObject ExecuteNonQueryDto(DALCHelper helper, DataTransferObject criteria)
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper cw = null;

            // Initialize output parameters				
            DataTransferObject dto = null;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.ExecuteNonQuery was passed a null DALCHelper.");

            // Make sure any necessary criteria has been set
            if (helper.CriteriaIsValid(criteria) == false)
                throw new MNNonFatalException("CommonDALC.ExecuteNonQuery was passed invalid data and/or criteria.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                cw = helper.InitializeCommand(db, criteria);
                DbCommand command = cw.Command;

                // Execute a data reader
                if (_isInTransaction)
                    db.ExecuteNonQuery(command, _transaction.GetTransaction());
                else
                    db.ExecuteNonQuery(command);

                // set command for garbage collection
                command = null;

                // Create return data transfer object with the key set
                dto = helper.CreateResultsDto(cw, criteria);
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.ExecuteNonQuery");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e, criteria);
                ex.AddMessageTraceData("Exception caught in CommonDALC.ExecuteNonQueryDto");
                throw ex;
            }
            finally
            {
                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrapper object for garbage collection
                if (cw != null)
                {
                    cw = null;
                }
            }

            // Return data transfer object
            return dto;
        }

        #endregion

        #region Update DataSet Methods

        /// <summary>
        /// This method updates the database based on modified data within the DataSet object that has been 
        /// submitted back to the database through this method.
        /// </summary>
        /// <param name="ds">The DataSet containing the modified data.</param>
        /// <param name="helper">The <see cref="DALCHelperDsUpdater"/> class that contains the database 
        /// command information.</param>
        /// <returns>An integer value of the number of records affected by the update.</returns>
        protected int UpdateDataSet(DataSet ds, DALCHelperDsUpdater helper)
        {
            // Initialize objects
            Database db = null;
            DbCommandWrapper commInsert = null;
            DbCommandWrapper commUpdate = null;
            DbCommandWrapper commDelete = null;

            // Initialize output parameters				
            int recordsAffected;

            // If the passed in helper is null throw exception
            if (null == helper)
                throw new MNNonFatalException("CommonDALC.UpdateDataSet was passed a null DALCHelperDsUpdater.");

            // If the passed in dataset is null throw exception
            if (null == ds)
                throw new MNNonFatalException("CommonDALC.UpdateDataSet was passed a null DataSet.");

            try
            {
                // Initialize the database and command wrapper
                if (helper.DbInstanceName() == null)
                    db = DatabaseFactory.CreateDatabase();
                else
                    db = DatabaseFactory.CreateDatabase(helper.DbInstanceName());

                commInsert = helper.InitializeInsertCommand(db);
                commUpdate = helper.InitializeUpdateCommand(db);
                commDelete = helper.InitializeDeleteCommand(db);

                // Update the database
                if (_isInTransaction)
                    recordsAffected =
                        db.UpdateDataSet(ds, helper.GetDsTableName(), commInsert.Command, commUpdate.Command,
                                         commDelete.Command, _transaction.GetTransaction());
                else
                    recordsAffected =
                        db.UpdateDataSet(ds, helper.GetDsTableName(), commInsert.Command, commUpdate.Command,
                                         commDelete.Command, GetUpdateBehavior(helper.GetDsUpdateBehavior()));
            }
            catch (MNException e)
            {
                e.AddMessageTraceData("MNException caught in CommonDALC.UpdateDataSet");
                throw;
            }
            catch (Exception e)
            {
                MNException ex = new MNException(e.Message, e);
                ex.AddMessageTraceData("Exception caught in CommonDALC.UpdateDataSet");
                throw ex;
            }
            finally
            {
                // Set the database object for garbage collection
                if (db != null)
                {
                    db = null;
                }

                // Set the command wrappers object for garbage collection
                if (commInsert != null)
                {
                    commInsert = null;
                }
                if (commUpdate != null)
                {
                    commUpdate = null;
                }
                if (commDelete != null)
                {
                    commDelete = null;
                }
            }

            // Return the number of records affected by the update
            return recordsAffected;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the Microsoft.Practices.EnterpriseLibrary.Data.UpdateBehavior for the DataSet.
        /// </summary>
        /// <param name="updateBehavior">The <see cref="DALCUpdateBehavior"/> integer values that 
        /// corresponds to update behaviors defining the behavior for transactional interference.</param>
        /// <returns>Microsoft.Practices.EnterpriseLibrary.Data.UpdateBehavior which provides the
        /// control over the behavior when the Data Adapter's update command encounters an error.</returns>
        private static UpdateBehavior GetUpdateBehavior(int updateBehavior)
        {
            switch (updateBehavior)
            {
                case DALCUpdateBehavior.STANDARD:
                    return UpdateBehavior.Standard;
                case DALCUpdateBehavior.CONTINUE:
                    return UpdateBehavior.Continue;
                case DALCUpdateBehavior.TRANSACTIONAL:
                    return UpdateBehavior.Transactional;
            }

            //return standard
            return UpdateBehavior.Standard;
        }

        #endregion
    }
}