using Survey.Utils;

namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionString : ProfileQuestion
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionString using the specified name and question.
        /// </summary>
        public QuestionString(string name, string question) : base(name, question)
        {
           
        }

        /// <summary>
        ///     Initializes a new instance of the QuestionString using the specified name , the question, the min length and the max length.
        /// </summary>
        public QuestionString(string name, string question, int minLength, int maxLength):base( name,  question)
        {
            MaxLength = maxLength;
            MinLength = minLength;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Max length.
        /// </summary>
        public int MaxLength { get; set; } = int.MaxValue;

        /// <summary>
        ///     The min length.
        /// </summary>
        public int MinLength { get; set; } = 0;

        #endregion Public Properties


        #region Public Methods

        /// <summary>
        ///     Checks the answer for correctness.
        /// </summary>
        /// <param name="answer"> the answer</param>
        /// <returns>the correct answer</returns>
        public override string CheckedAnswer(string answer)
        {
            string answerText = "";

            if (answer.Length>= MaxLength)
                throw new SurveyException(ErrorMessages.MaxLenght);

            if (answer.Length <= MinLength)
                throw new SurveyException(ErrorMessages.MinLenght);
            
                answerText = answer;

            return answerText;
        }

        #endregion Public Methods


    }
}
