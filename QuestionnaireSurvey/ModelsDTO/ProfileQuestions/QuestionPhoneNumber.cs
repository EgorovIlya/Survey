using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.ModelsDTO.ProfileQuestions
{
    public class QuestionPhoneNumber:ProfileQuestion
    {
        #region Constructors

        public QuestionPhoneNumber(string name, string question) : base(name, question) {}

        #endregion Constructors

        #region Public Methods

        /// <summary>
        ///     Checks answer for correctness.
        /// </summary>
        /// <param name="answer"> answer</param>
        /// <returns>correct answer</returns>
        public override string CheckedAnswer(string answer)
        {
          Regex phonePattern = new Regex(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");

            if (!phonePattern.IsMatch(answer))
            {
                throw new SurveyException(ErrorMessages.PhoneNumberIsNotCorrect);
            }

            return answer;
        }

        #endregion Public Methods
    }
}
