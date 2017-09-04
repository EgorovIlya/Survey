namespace QuestionnaireSurvey.ModelsDTO.ProfileQuestions
{
    public abstract class ProfileQuestion
    {

        #region Constructors

        protected ProfileQuestion(string name,string question)
        {
            QuestionName = name;
            QuestionText = question;
        }

        #endregion Constructors

        #region Public Properties
        /// <summary>
        ///     Question name.
        /// </summary>
        public string QuestionName { get; set; }

        /// <summary>
        ///     Question text.
        /// </summary>
        public string QuestionText { get; set; }

        #endregion Public Properties

        #region Public Methods
        /// <summary>
        ///     Checks answer for correctness.
        /// </summary>
        /// <param name="answer"> answer</param>
        /// <returns></returns>
        public abstract string CheckedAnswer(string answer);
        
        #endregion Public Methods
    }
}
