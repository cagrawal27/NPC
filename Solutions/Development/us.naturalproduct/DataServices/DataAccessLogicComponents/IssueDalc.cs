using System;
using System.Collections.Generic;
using System.Text;
using MN.Enterprise.Data;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    /// <summary>
    /// This class is a DataAccessLogicComponent for retrieving Issue related data.
    /// It calls specific Issue related helper classes.
    /// </summary>
    /// <author>Monish Nagisetty</author>
    /// <created>01/04/2007</created>
    /// <updated>01/04/2007</updated>
    // Revision History
    //
    //=============================================================================
    // Change   Initial Date        Description
    //=============================================================================
    public class IssueDalc: CommonDALC
    {
        public IssueDalc()
        {
        }

        public IssueDalc(DALCTransaction transaction) : base(transaction)
        {
        }

        public Issue GetIssueAndVolumeName(Issue issueDto)
        {
            return ExecuteQueryDto(new VolumeAndIssueSelectHelper(), issueDto) as Issue;
        }
    }
}
