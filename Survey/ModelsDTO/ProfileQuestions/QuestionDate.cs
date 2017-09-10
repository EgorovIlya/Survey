using System;
using System.Globalization;
using Survey.Utils;

namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionDate:ProfileQuestion
    {
        #region Constructors

        /// <summary>
        ///     Constructs  a new instance of the QuestionDate using the specified name and the question.
        /// </summary>
        public QuestionDate(string name, string question) : base(name, question)
        {
        }

        /// <summary>
        ///     Constructs  a new instance of the QuestionDate using the specified name , the question, the min date and the max date.
        /// </summary>
        public QuestionDate(string name, string question, DateTime min, DateTime max ) : base(name, question)
        {
            MaxDate = max;
            MinDate = min;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Max date.
        /// </summary>
        public DateTime MaxDate { get; set; } = DateTime.MaxValue;

        /// <summary>
        ///     Min date.
        /// </summary>
        public DateTime MinDate { get; set; } = DateTime.MinValue;

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
            DateTime date;

            if (!DateTime.TryParseExact(answer, SurveyConst.FormatDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
                throw new SurveyException(ErrorMessages.DateFormatError);

            if (date > MaxDate)
                throw new SurveyException($"{ErrorMessages.DateMustBeLess} {MaxDate.ToString(SurveyConst.FormatDate)}!");

            if (date < MinDate)
                throw new SurveyException($"{ErrorMessages.DateMustBeGreater} {MinDate.ToString(SurveyConst.FormatDate)}!");

            answerText = answer;
            return answerText;
        }

        #endregion Public Methods


    }
}
