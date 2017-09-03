using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.ModelsDTO.ProfileQuestions
{
    public class QuestionValuesList:ProfileQuestion
    {
        public QuestionValuesList(string name, string question, List<string> values) : base(name, question)
        {
            ChekingValues = values;
        }

        /// <summary>
        ///     Checks answer for correctness.
        /// </summary>
        /// <param name="answer"> answer</param>
        /// <returns>correct answer</returns>
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

        public List<string> ChekingValues { get; set; }
    }
}
