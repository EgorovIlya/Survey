using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSurvey.Utils
{
    public static class Tools
    {
        public static string GetYears(int years)
        {
            if ((years - 11) % 100 == 0)
                return "лет";

            if ((years - 12) % 100 == 0)
                return "лет";

            if ((years - 13) % 100 == 0)
                return "лет";

            if ((years - 14) % 100 == 0)
                return "лет";

            if ((years - 1) % 10 == 0)
                return "год";

            if ((years - 2) % 10 == 0)
                return "года";

            if ((years - 3) % 10 == 0)
                return "года";

            if ((years - 4) % 10 == 0)
                return "года";
            
                return "лет";
        }

        public static string GetTextWithoutCommand(string text, string command)
        {
            string result = "";

            if(!text.Contains(command))
                throw new SurveyException(ErrorMessages.InputTextWithOutCommand);

            int commandLenght = command.Length;
            if(text.Length> commandLenght)
                  result = text.Substring(commandLenght, text.Length - commandLenght).TrimStart().TrimEnd();
            else
            {
                throw new SurveyException(ErrorMessages.CommandMustBeWithParametrs);
            }

            return result;
        }
    }
}
