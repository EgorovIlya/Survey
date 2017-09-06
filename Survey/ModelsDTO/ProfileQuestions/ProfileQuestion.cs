namespace Survey.ModelsDTO.ProfileQuestions
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
        ///     Represents the name of the question.
        /// </summary>
        public string QuestionName { get; set; }

        /// <summary>
        ///     Represents the text of the question.
        /// </summary>
        public string QuestionText { get; set; }

        #endregion Public Properties

        #region Public Methods
        /// <summary>
        ///     Checks the answer for correctness.
        /// </summary>
        /// <param name="answer">the answer</param>
        /// <returns>the checked answer</returns>
        public abstract string CheckedAnswer(string answer);
        
        #endregion Public Methods
    }
}
