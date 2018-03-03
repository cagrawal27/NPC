using System;
using System.Collections.Generic;
using System.Text;

namespace us.naturalproduct.DataTransferObjects
{
    public class PasswordRecoveryQuestion: BaseObject
    {
        public PasswordRecoveryQuestion()
        {
        }

        private Int32 questionId;
        private string description;
        private string answer;

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }
    }
}
