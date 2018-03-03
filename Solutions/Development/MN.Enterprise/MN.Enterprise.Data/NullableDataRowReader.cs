using System;
using System.Data;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// This class allows a DataRow to be read with a datareader-like interface.
    /// Since it implements INullableRader, it provides methods to read both
    /// non-null and nullable data fields.
    /// </summary>
    public class NullableDataRowReader : INullableReader
    {
        #region Private Fields

        private DataRow row;

        /// <summary>
        /// Delegate to be used for anonymous method delegate inference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private delegate T Conversion<T>(string name);

        #endregion

        #region Public Properties

        public DataRow Row
        {
            get { return row; }
            set { row = value; }
        }

        #endregion

        #region Constructors

        public NullableDataRowReader(DataRow dataRow)
        {
            row = dataRow;
        }

        public NullableDataRowReader()
        {
        }

        #endregion

        #region INullableReader Members

        public bool GetBoolean(string name)
        {
            return Convert.ToBoolean(row[name]);
        }

        public bool? GetNullableBoolean(string name)
        {
            return GetNullable<bool>(name, GetBoolean);
        }

        public byte GetByte(string name)
        {
            return Convert.ToByte(row[name]);
        }

        public byte? GetNullableByte(string name)
        {
            return GetNullable<byte>(name, GetByte);
        }

        public char GetChar(string name)
        {
            return Convert.ToChar(row[name]);
        }

        public char? GetNullableChar(string name)
        {
            return GetNullable<char>(name, GetChar);
        }

        public DateTime GetDateTime(string name)
        {
            return Convert.ToDateTime(row[name]);
        }

        public DateTime? GetNullableDateTime(string name)
        {
            return GetNullable<DateTime>(name, GetDateTime);
        }

        public decimal GetDecimal(string name)
        {
            return Convert.ToDecimal(row[name]);
        }

        public decimal? GetNullableDecimal(string name)
        {
            return GetNullable<decimal>(name, GetDecimal);
        }

        public double GetDouble(string name)
        {
            return Convert.ToDouble(row[name]);
        }

        public double? GetNullableDouble(string name)
        {
            return GetNullable<double>(name, GetDouble);
        }

        public float GetFloat(string name)
        {
            return Convert.ToSingle(row[name]);
        }

        public float? GetNullableFloat(string name)
        {
            return GetNullable<float>(name, GetFloat);
        }

        public Guid GetGuid(string name)
        {
            return (Guid) row[name];
        }

        public Guid? GetNullableGuid(string name)
        {
            return GetNullable<Guid>(name, GetGuid);
        }

        public short GetInt16(string name)
        {
            return Convert.ToInt16(row[name]);
        }

        public short? GetNullableInt16(string name)
        {
            return GetNullable<short>(name, GetInt16);
        }

        public int GetInt32(string name)
        {
            return Convert.ToInt32(row[name]);
        }

        public int? GetNullableInt32(string name)
        {
            return GetNullable<int>(name, GetInt32);
        }

        public long GetInt64(string name)
        {
            return Convert.ToInt64(row[name]);
        }

        public long? GetNullableInt64(string name)
        {
            return GetNullable<long>(name, GetInt64);
        }

        public string GetString(string name)
        {
            return row[name].ToString();
        }

        public string GetNullableString(string name)
        {
            if (row[name] == DBNull.Value)
            {
                return null;
            }
            else
            {
                return GetString(name);
            }
        }

        public object GetValue(string name)
        {
            return row[name];
        }

        public bool IsDBNull(string name)
        {
            return (row[name] == DBNull.Value);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This generic method will be call by every interface method in the class.
        /// The generic method will offer significantly less code, with type-safety.
        /// Additionally, the methods can you delegate inference to pass the 
        /// appropriate delegate to be executed in this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Column name</param>
        /// <param name="convert">Delegate to invoke if the value is not DBNull</param>
        /// <returns></returns>
        private Nullable<T> GetNullable<T>(string name, Conversion<T> convert) where T : struct
        {
            Nullable<T> nullable;
            if (row[name] == DBNull.Value)
            {
                nullable = null;
            }
            else
            {
                nullable = convert(name);
            }
            return nullable;
        }

        #endregion
    }
}