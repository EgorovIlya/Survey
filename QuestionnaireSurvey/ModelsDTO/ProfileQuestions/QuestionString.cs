using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.ModelsDTO.ProfileQuestions
{
    public class QuestionString : ProfileQuestion
    {
        #region Constructors

        public QuestionString(string name, string question) : base(name, question)
        {
           
        }

        public QuestionString(string name, string question, int minLength, int maxLength):base( name,  question)
        {
            MaxLength = maxLength;
            MinLength = minLength;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Max lenght.
        /// </summary>
        public int MaxLength { get; set; } = int.MaxValue;

        /// <summary>
        ///     Min lenght.
        /// </summary>
        public int MinLength { get; set; } = 0;

        #endregion Public Properties


        #region Public Methods

        /// <summary>
        ///     Checks answer for correctness.
        /// </summary>
        /// <param name="answer"> answer</param>
        /// <returns>correct answer</returns>
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
