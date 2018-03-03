using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace us.naturalproduct.Common
{
    public static class Constants
    {
        public static string[] stateCode = { "", "NA", "AL", "AK", "AB", "AZ", "AR", "BC", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MB", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NB", "NF", "NH", "NJ", "NM", "NY", "NC", "ND", "NS", "OH", "OK", "ON", "OR", "PA", "PE", "PQ", "RI", "SK", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };

        public static string[] stateName = { "-- Select --", "Other", "Alabama", "Alaska", "Alberta", "Arizona", "Arkansas", "British Columbia", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Manitoba", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Brunswick", "Newfoundland", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Nova Scotia", "Ohio", "Oklahoma", "Ontario", "Oregon", "Pennsylvania", "Prince Edward Island", "Quebec", "Rhode Island", "Saskatchewan", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };

        public static string StateName = "StateName";
        public static string StateCode = "StateCode";

        public static string CountryName = "CountryName";
        public static string CountryId = "CountryId";

        public static Int32 Account_Status_Active = 1;
        public static Int32 Account_Status_Locked = 2;
        public static Int32 Account_Status_Stale = 3;

        public static Int32 Art_Doc_Type_Abstract = 1;
        public static Int32 Art_Doc_Type_FullText = 2;
        public static Int32 Art_Doc_Type_Supplementary = 3;

        public static Int32 Address_Type_Default = 1;
        public static Int32 Address_Type_Billing = 2;

        public static Int32 Account_Type_Personal = 1;
        public static Int32 Account_Type_Institutional = 2;

        public static Int32 Anonymous_Web_User_Id = 1;

        public static Int32 Role_Member = 1;
        public static Int32 Role_Admin = 2;

        public static DataTable GetStates()
        {
            DataTable tblStates = new DataTable("States");
            DataColumn ColStateCode = new DataColumn(StateCode, Type.GetType("System.String"));
            DataColumn ColStateName = new DataColumn(StateName, Type.GetType("System.String"));
            DataRow drState;

            tblStates.Columns.Add(ColStateCode);
            tblStates.Columns.Add(ColStateName);

            for (int i = 0; i < stateCode.Length; i++)
            {
                drState = tblStates.NewRow();

                drState[StateCode] = stateCode[i];

                drState[StateName] = stateName[i];

                tblStates.Rows.Add(drState);
            }

            return tblStates;


        }

    }
}
