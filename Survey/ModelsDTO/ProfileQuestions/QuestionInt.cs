using Survey.Utils;


namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionInt:ProfileQuestion
    {

        #region Constructors

        public QuestionInt(string name, string question) : base(name, question)
        {
        }

        public QuestionInt(string name, string question, int min, int max) : base(name, question)
        {
            MaxValue = max;
            MinValue = min;
        }

        #endregion Constructors

        #region Public Properties

        public int MaxValue { get; set; } = int.MaxValue;

        public int MinValue { get; set; } = int.MinValue;

        #endregion Public Properties

        #region Public Methods
        /// <summary>
        ///     Checks answer for correctness.
        /// </summary>
        /// <param name="answer"> answer</param>
        /// <returns>correct answer</returns>
        public override string CheckedAnswer(string answer)
        {
            int answerValue;

            if (!int.TryParse(answer, out answerValue))
            {
                throw new SurveyException(ErrorMessages.IntFormatError);
            }

            if (answerValue > MaxValue)
            {
                throw new SurveyException($"{ErrorMessages.NumberMustBeLessMax}{MaxValue}!");
            }

            if (answerValue < MinValue)
            {
                throw new SurveyException($"{ErrorMessages.NumberMustBeGreaterMin}{MinValue}!");
            }

            return answer;
        }

        #endregion Public Methods


    }
}
