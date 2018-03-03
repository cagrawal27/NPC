using System;
using System.Data;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// This class is a DataReader that provides the ability to seamlessly work
    /// with Nullable types as well as non-Nullable types.
    /// 
    /// The first version of ADO.NET 2.0 does not natively support nullable types
    /// (although this is cleary supported in the run-time).  Therefore, a simple
    /// call like this:
    /// <code>
    /// Nullable<DateTime> someDate = dataReader.GetDateTime(0);
    /// </code>
    /// will throw an exception when the database column contains a null value.
    /// 
    /// This class wraps an IDataReader (which it takes in its constructor) and
    /// 1) provides alternatives to all the GetXXX() methods with GetNullableXXX() methods
    /// 2) provides the ability to use column name in the GetXXX() methods (rather than 
    ///    having to specify the ordinal)
    /// 3) provides all the traditional IDataReader methods by delegating the calls to
    ///    the IDataReader that it wraps.
    /// Author: Steve Michelotti
    /// </summary>
    public sealed class NullableDataReader : IDataReader, INullableReader
    {
        #region Private Fields

        IDataReader reader;

        /// <summary>
        /// Delegate to be used for anonymous method delegate inference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private delegate T Conversion<T>(int ordinal);

        #endregion

        #region Constructors

        public NullableDataReader(IDataReader dataReader)
        {
            reader = dataReader;
        }

        private NullableDataReader()
        {
        }

        #endregion

        #region IDataReader Members

        public void Close()
        {
            reader.Close();
        }

        public int Depth
        {
            get { return reader.Depth; }
        }

        public DataTable GetSchemaTable()
        {
            return reader.GetSchemaTable();
        }

        public bool IsClosed
        {
            get { return reader.IsClosed; }
        }

        public bool NextResult()
        {
            return reader.NextResult();
        }

        public bool Read()
        {
            return reader.Read();
        }

        public int RecordsAffected
        {
            get { return reader.RecordsAffected; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (reader != null)
            {
                reader.Dispose();
            }
        }

        #endregion

        #region IDataRecord Members

        public int FieldCount
        {
            get { return reader.FieldCount; }
        }

        public bool GetBoolean(int i)
        {
            return reader.GetBoolean(i);
        }

        public bool GetBoolean(string name)
        {
            return GetBoolean(reader.GetOrdinal(name));
        }

        public Nullable<bool> GetNullableBoolean(int index)
        {
            return GetNullable<bool>(index, GetBoolean);
        }

        public Nullable<bool> GetNullableBoolean(string name)
        {
            return GetNullableBoolean(reader.GetOrdinal(name));
        }

        public byte GetByte(int i)
        {
            return reader.GetByte(i);
        }

        public byte GetByte(string name)
        {
            return GetByte(reader.GetOrdinal(name));
        }

        public Nullable<byte> GetNullableByte(int index)
        {
            return GetNullable<byte>(index, GetByte);
        }

        public Nullable<byte> GetNullableByte(string name)
        {
            return GetNullableByte(reader.GetOrdinal(name));
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return reader.GetChar(i);
        }

        public char GetChar(string name)
        {
            return GetChar(reader.GetOrdinal(name));
        }

        public Nullable<char> GetNullableChar(int index)
        {
            return GetNullable<char>(index, GetChar);
        }

        public Nullable<char> GetNullableChar(string name)
        {
            return GetNullableChar(reader.GetOrdinal(name));
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /**
         * According to MS documentation, this is an infrastructural method that
         * is not intended to be used by public code.
         **/

        public IDataReader GetData(int i)
        {
            return reader.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return reader.GetDataTypeName(i);
        }

        public string GetDataTypeName(string name)
        {
            return reader.GetDataTypeName(reader.GetOrdinal(name));
        }

        public DateTime GetDateTime(int i)
        {
            return reader.GetDateTime(i);
        }

        public DateTime GetDateTime(string name)
        {
            return reader.GetDateTime(reader.GetOrdinal(name));
        }

        public Nullable<DateTime> GetNullableDateTime(int index)
        {
            return GetNullable<DateTime>(index, GetDateTime);
        }

        public Nullable<DateTime> GetNullableDateTime(string name)
        {
            return GetNullableDateTime(reader.GetOrdinal(name));
        }

        public decimal GetDecimal(int i)
        {
            return reader.GetDecimal(i);
        }

        public decimal GetDecimal(string name)
        {
            return reader.GetDecimal(reader.GetOrdinal(name));
        }

        public Nullable<Decimal> GetNullableDecimal(int index)
        {
            return GetNullable<decimal>(index, GetDecimal);
        }

        public Nullable<Decimal> GetNullableDecimal(string name)
        {
            return GetNullableDecimal(reader.GetOrdinal(name));
        }

        public double GetDouble(int i)
        {
            return reader.GetDouble(i);
        }

        public double GetDouble(string name)
        {
            return reader.GetDouble(reader.GetOrdinal(name));
        }

        public Nullable<double> GetNullableDouble(int index)
        {
            return GetNullable<double>(index, GetDouble);
        }

        public Nullable<double> GetNullableDouble(string name)
        {
            return GetNullableDouble(reader.GetOrdinal(name));
        }

        public Type GetFieldType(int i)
        {
            return reader.GetFieldType(i);
        }

        public Type GetFieldType(string name)
        {
            return reader.GetFieldType(reader.GetOrdinal(name));
        }

        public float GetFloat(int i)
        {
            return reader.GetFloat(i);
        }

        public float GetFloat(string name)
        {
            return reader.GetFloat(reader.GetOrdinal(name));
        }

        public Nullable<float> GetNullableFloat(int index)
        {
            return GetNullable<float>(index, GetFloat);
        }

        public Nullable<float> GetNullableFloat(string name)
        {
            return GetNullableFloat(reader.GetOrdinal(name));
        }

        public Guid GetGuid(int i)
        {
            return reader.GetGuid(i);
        }

        public Guid GetGuid(string name)
        {
            return reader.GetGuid(reader.GetOrdinal(name));
        }

        public Nullable<Guid> GetNullableGuid(int index)
        {
            return GetNullable<Guid>(index, GetGuid);
        }

        public Nullable<Guid> GetNullableGuid(string name)
        {
            return GetNullableGuid(reader.GetOrdinal(name));
        }

        public short GetInt16(int i)
        {
            return reader.GetInt16(i);
        }

        public short GetInt16(string name)
        {
            return reader.GetInt16(reader.GetOrdinal(name));
        }

        public Nullable<short> GetNullableInt16(int index)
        {
            return GetNullable<short>(index, GetInt16);
        }

        public Nullable<short> GetNullableInt16(string name)
        {
            return GetNullableInt16(reader.GetOrdinal(name));
        }

        public int GetInt32(int i)
        {
            return reader.GetInt32(i);
        }

        public int GetInt32(string name)
        {
            return reader.GetInt32(reader.GetOrdinal(name));
        }

        public Nullable<int> GetNullableInt32(int index)
        {
            return GetNullable<int>(index, GetInt32);
        }

        public Nullable<int> GetNullableInt32(string name)
        {
            return GetNullableInt32(reader.GetOrdinal(name));
        }

        public long GetInt64(int i)
        {
            return reader.GetInt64(i);
        }

        public long GetInt64(string name)
        {
            return reader.GetInt64(reader.GetOrdinal(name));
        }

        public Nullable<long> GetNullableInt64(int index)
        {
            return GetNullable<long>(index, GetInt64);
        }

        public Nullable<long> GetNullableInt64(string name)
        {
            return GetNullableInt64(reader.GetOrdinal(name));
        }

        public string GetName(int i)
        {
            return reader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return reader.GetOrdinal(name);
        }

        public string GetString(int i)
        {
            return reader.GetString(i);
        }

        public string GetString(string name)
        {
            return reader.GetString(reader.GetOrdinal(name));
        }

        public string GetNullableString(int index)
        {
            string nullable;
            if (reader.IsDBNull(index))
            {
                nullable = null;
            }
            else
            {
                nullable = reader.GetString(index);
            }
            return nullable;
        }

        public string GetNullableString(string name)
        {
            return GetNullableString(reader.GetOrdinal(name));
        }

        public object GetValue(int i)
        {
            return reader.GetValue(i);
        }

        public object GetValue(string name)
        {
            return reader.GetValue(reader.GetOrdinal(name));
        }

        public int GetValues(object[] values)
        {
            return reader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return reader.IsDBNull(i);
        }

        public bool IsDBNull(string name)
        {
            return reader.IsDBNull(reader.GetOrdinal(name));
        }

        public object this[string name]
        {
            get { return reader[name]; }
        }

        public object this[int i]
        {
            get { return reader[i]; }
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
        /// <param name="ordinal">Column index.</param>
        /// <param name="convert">Delegate to invoke if the value is not DBNull</param>
        /// <returns></returns>
        private Nullable<T> GetNullable<T>(int ordinal, Conversion<T> convert) where T : struct
        {
            Nullable<T> nullable;
            if (reader.IsDBNull(ordinal))
            {
                nullable = null;
            }
            else
            {
                nullable = convert(ordinal);
            }
            return nullable;
        }

        #endregion
    }
}