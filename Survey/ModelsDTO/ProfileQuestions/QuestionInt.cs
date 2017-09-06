using Survey.Utils;


namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionInt:ProfileQuestion
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionInt using the specified name and question.
        /// </summary>
        public QuestionInt(string name, string question) : base(name, question)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the QuestionInt using the specified name , the question, the min value and the max value.
        /// </summary>
        public QuestionInt(string name, string question, int min, int max) : base(name, question)
        {
            MaxValue = max;
            MinValue = min;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents the max value. 
        /// </summary>
        public int MaxValue { get; set; } = int.MaxValue;

        /// <summary>
        ///     Represents the min value.
        /// </summary>
        public int MinValue { get; set; } = int.MinValue;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Checks the answer for correctness.
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
