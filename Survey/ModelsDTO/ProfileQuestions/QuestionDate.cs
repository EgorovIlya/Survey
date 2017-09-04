using System;
using System.Globalization;
using Survey.Utils;

namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionDate:ProfileQuestion
    {
        #region Constructors

        public QuestionDate(string name, string question) : base(name, question)
        {
        }

        public QuestionDate(string name, string question, DateTime min, DateTime max ) : base(name, question)
        {
            MaxDate = max;
            MinDate = min;
        }

        #endregion Constructors

        #region Public Properties

        public DateTime MaxDate { get; set; } = DateTime.MaxValue;

        public DateTime MinDate { get; set; } = DateTime.MinValue;

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

            DateTime date;

            if (!DateTime.TryParseExact(answer, SurveyConst.FormatDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            {
                throw new SurveyException(ErrorMessages.DateFormatError);
            }

            if (date > MaxDate)
            {
                throw new SurveyException($"{ErrorMessages.DateMustBeLess} {MaxDate.ToString(SurveyConst.FormatDate)}!");
            }

            if (date < MinDate)
            {
                throw new SurveyException($"{ErrorMessages.DateMustBeGreater} {MinDate.ToString(SurveyConst.FormatDate)}!");
            }

            answerText = answer;

            return answerText;
        }

        #endregion Public Methods


    }
}
