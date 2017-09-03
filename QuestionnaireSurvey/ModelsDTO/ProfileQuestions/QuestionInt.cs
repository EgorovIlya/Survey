using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.ModelsDTO.ProfileQuestions
{
    public class QuestionInt:ProfileQuestion
    {
        public QuestionInt(string name, string question) : base(name, question)
        {
        }

        public QuestionInt(string name, string question, int min, int max) : base(name, question)
        {
            MaxValue = max;
            MinValue = min;
        }

        public int MaxValue { get; set; } = int.MaxValue;

        public int MinValue { get; set; } = int.MinValue;

        /// <summary>
        ///     Checks answer for correctness.
        /// </summary>
        /// <param name="answer"> answer</param>
        /// <returns>correct answer</returns>
        public override string CheckedAnswer(string answer)
        {
            int answerValue;

            if (!int.TryParse(answer, out answerValue))
            {
                throw new SurveyException(ErrorMessages.IntFormatError);
            }

            if (answerValue > MaxValue)
            {
                throw new SurveyException($"{ErrorMessages.NumberMustBeLessMax}{MaxValue}!");
            }

            if (answerValue < MinValue)
            {
                throw new SurveyException($"{ErrorMessages.NumberMustBeGreaterMin}{MinValue}!");
            }

            return answer;
        }

        
    }
}
