using System.Collections.Generic;
using MN.Enterprise.Data;
using us.naturalproduct.DALCQueryHelpers;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DataAccessLogicComponents
{
    public class PasswordRecoveryQuestionDalc : CommonDALC
    {
        public List<PasswordRecoveryQuestion> GetPasswordQuestions(User userDto)
        {
            return ExecuteQueryList<PasswordRecoveryQuestion>(new PasswordQuestionListByEmailSelectHelper(), userDto);
        }
    }
}