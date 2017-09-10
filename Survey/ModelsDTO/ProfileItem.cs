using System;
using Survey.ModelsDTO.ProfileQuestions;

namespace Survey.ModelsDTO
{
    /// <summary>
    ///     Represents a ProfileItem that consists of ProfileQuestion and from Answer.
    /// </summary>
    public class ProfileItem
    {
        #region Constructors
        /// <summary>
        ///      Initializes a new instance of the ProfileItem using the specified ProfileQuestion.
        /// </summary>
        /// <param name="question">the specified ProfileQuestion.</param>
        public ProfileItem(ProfileQuestion question)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
        }

        /// <summary>
        ///      Initializes a new instance of the ProfileItem using the specified ProfileQuestion and the answer.
        /// </summary>
        /// <param name="question">the specified ProfileQuestion.</param>
        /// <param name="answer">the answer</param>
        public ProfileItem(ProfileQuestion question, string answer ):this( question)
        {
            Answer = answer ?? throw new ArgumentNullException(nameof(answer)); 
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents a ProfileQuestion.
        /// </summary>
        public ProfileQuestion Question { get; set; }

        /// <summary>
        ///     Represents an answer.
        /// </summary>
        public string Answer { get; set; } = "";

        #endregion Public Properties
    }
}
