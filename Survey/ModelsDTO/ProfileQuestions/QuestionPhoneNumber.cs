using System.Text.RegularExpressions;
using Survey.Utils;

namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionPhoneNumber:ProfileQuestion
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionPhoneNumber using the specified name and question.
        /// </summary>
        public QuestionPhoneNumber(string name, string question) : base(name, question) {}

        #endregion Constructors

        #region Public Methods

        /// <summary>
        ///     Checks the answer for correctness.
        /// </summary>
        /// <param name="answer"> the answer</param>
        /// <returns>the correct answer</returns>
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
