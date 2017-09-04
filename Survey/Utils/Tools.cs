namespace Survey.Utils
{

    /// <summary>
    ///      Initializes a new instance of the FileWorker using the specified command name.
    /// </summary>
   public static class Tools
    {

        /// <summary>
        ///     Returns text, that corresponds to the number of years.
        /// </summary>
        /// <param name="years">the number of years</param>
        /// <returns></returns>
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
        ///     Returns user input without command.
        /// </summary>
        /// <param name="text">user input</param>
        /// <param name="command">command</param>
        /// <returns></returns>
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
