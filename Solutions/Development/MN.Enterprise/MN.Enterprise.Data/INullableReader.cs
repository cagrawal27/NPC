using System;

namespace MN.Enterprise.Data
{
    /// <summary>
    /// This interface defines the contract that a class must implement to read
    /// both non-null and nullable data.  The greatest benefit of this interface
    /// is that, since both NullableDataReader and NullableDataRowReader 
    /// implement it, this interface will allow these classes to be used 
    /// polymorphically.  In other words, if the consumer consumer need to 
    /// populate an object from both a IDataReader and/or a DataRow (from a
    /// DataSet) then they can just write 1 polymorphic method for this by
    /// programming against the INullableReader API rather than having to create
    /// two separate methods.
    /// Author: Steve Michelotti
    /// </summary>
    public interface INullableReader
    {
        #region Interface Methods

        bool GetBoolean(string name);
        Nullable<bool> GetNullableBoolean(string name);
        byte GetByte(string name);
        Nullable<byte> GetNullableByte(string name);
        char GetChar(string name);
        Nullable<char> GetNullableChar(string name);
        DateTime GetDateTime(string name);
        Nullable<DateTime> GetNullableDateTime(string name);
        decimal GetDecimal(string name);
        Nullable<Decimal> GetNullableDecimal(string name);
        double GetDouble(string name);
        Nullable<double> GetNullableDouble(string name);
        float GetFloat(string name);
        Nullable<float> GetNullableFloat(string name);
        Guid GetGuid(string name);
        Nullable<Guid> GetNullableGuid(string name);
        short GetInt16(string name);
        Nullable<short> GetNullableInt16(string name);
        int GetInt32(string name);
        Nullable<int> GetNullableInt32(string name);
        long GetInt64(string name);
        Nullable<long> GetNullableInt64(string name);
        string GetString(string name);
        string GetNullableString(string name);
        object GetValue(string name);
        bool IsDBNull(string name);

        #endregion
    }
}