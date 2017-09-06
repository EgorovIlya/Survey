using System.Collections.Generic;
using System.Text;
using Survey.Utils;

namespace Survey.ModelsDTO.ProfileQuestions
{
    public class QuestionValuesList:ProfileQuestion
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionString using the specified name and question.
        /// </summary>
        public QuestionValuesList(string name, string question, List<string> values) : base(name, question)
        {
            ChekingValues = values;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents the list of the possible values.
        /// </summary>
        public List<string> ChekingValues { get; set; }

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

            if (ChekingValues.IndexOf(answer) == -1)
            {
                StringBuilder valuesList =  new StringBuilder();
                foreach (var value in ChekingValues)
                {
                    valuesList.Append(value);
                    valuesList.Append(",");
                }
                valuesList.Remove(valuesList.Length - 1, 1);
                throw new SurveyException($"{ErrorMessages.AnswerIsNotInTheList} {valuesList}!");
            }
            else
            {
                answerText = answer;
            }

            return answerText;
        }

        #endregion Public Methods

    }
}
