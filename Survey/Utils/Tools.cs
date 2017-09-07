using System;

namespace Survey.Utils
{

   public static class Tools
    {

        /// <summary>
        ///     Returns the text corresponding to the number of years
        /// </summary>
        /// <param name="years">the number of years</param>
        /// <returns>the text</returns>
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

        /// <summary>
        ///     Returns the user input without command.
        /// </summary>
        /// <param name="text">user input</param>
        /// <param name="command">command</param>
        /// <returns>the user input without command</returns>
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

        /// <summary>
        ///     Gets the clear user answer form the specified string.
        /// </summary>
        /// <param name="answer">the saved answer</param>
        /// <param name="separator">the separator</param>
        /// <returns>the answer without technical text</returns>
        public static string GetAnswerFormSavedProfile(string answer, string separator)
        {
            int lenght = answer.Length;
            int indexSeparator = answer.IndexOf(separator, StringComparison.Ordinal) + 2;
            int delta = lenght - indexSeparator;

            if (indexSeparator > lenght)
                return "";

            return answer.Substring(indexSeparator, delta);
        }
    }
}
