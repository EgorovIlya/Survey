using System;

namespace Survey.ModelsDTO.ProfileQuestions
{
    /// <summary>
    ///     Represents the ProfileQuestion. Consists from question name, question text,  and method, that checks for correctness.
    /// </summary>
    public abstract class ProfileQuestion
    {

        #region Constructors
        /// <summary>
        ///     Constructs a ProfileQuestion with a given name and question.
        /// </summary>
        /// <param name="name">specified name</param>
        /// <param name="question">specified question</param>
        protected ProfileQuestion(string name,string question)
        {
            QuestionName = name ?? throw new ArgumentNullException(nameof(name));
            QuestionText = question ?? throw new ArgumentNullException(nameof(question)); ;
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
