using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MN.Enterprise.Base;
using MN.Enterprise.Data;
using us.naturalproduct.DataTransferObjects;

namespace us.naturalproduct.DALCQueryHelpers
{
    public class PasswordQuestionListByEmailSelectHelper : DALCHelper<PasswordRecoveryQuestion>
    {
        //TODO:  Add New stored procedure to TEST/PROD DB:  spGetPasswordQuestionsByEmail
        public PasswordQuestionListByEmailSelectHelper()
        {
        }

        public override DbCommandWrapper InitializeCommand(Database db, DataTransferObject criteria)
        {
            User userDto = (User) criteria;

            DbCommandWrapper cw = DbCommandFactory.GetStoredProcCommandWrapper(db, "spGetPasswordQuestionsByEmail");

            cw.AddInParameter("@Email", DbType.String, userDto.EmailAddress);

            return cw;
        }

        public override List<PasswordRecoveryQuestion> ConvertResultsList(DbCommandWrapper cw, IDataReader reader)
        {
            if (reader == null)
                return null;

            List<PasswordRecoveryQuestion> passwordRecoveryQuestions = new List<PasswordRecoveryQuestion>();

            while (reader.Read())
            {
                PasswordRecoveryQuestion passwordRecoveryQuestion = new PasswordRecoveryQuestion();

                passwordRecoveryQuestion.QuestionId = (Int32) reader["QuestionId"];

                passwordRecoveryQuestion.Description = (string) reader["Description"];

                passwordRecoveryQuestions.Add(passwordRecoveryQuestion);
            }

            if (passwordRecoveryQuestions.Count == 0)
                return null;

            return passwordRecoveryQuestions;
        }
    }
}