using System;
using System.Collections.Generic;
using MN.Enterprise.Base;
using us.naturalproduct.DataAccessLogicComponents;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.BusinessServices.BusinessFacades
{
    public class PasswordRecoveryQuestionFacade
    {
        public List<PasswordRecoveryQuestion> GetPasswordQuestions(User inUserDto)
        {
            List<PasswordRecoveryQuestion> passwordRecoveryQuestions;

            try
            {
                PasswordRecoveryQuestionDalc userDalc = new PasswordRecoveryQuestionDalc();

                passwordRecoveryQuestions = userDalc.GetPasswordQuestions(inUserDto);
            }
            catch (MNException mnEx)
            {
                //TODO:  Log error
                throw mnEx;
            }
            catch (Exception ex)
            {
                //TODO:  Log error
                throw ex;
            }

            return passwordRecoveryQuestions;
        }
    }
}