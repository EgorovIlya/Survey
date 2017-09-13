using System;

namespace Survey.Utils
{
    public class UserInput
    {
        public UserInput(string command, string text)
        {
            FullText = text ?? throw new ArgumentNullException(nameof(text));
            CommandName = command ?? throw new ArgumentNullException(nameof(command));

            if (!text.StartsWith(command))
                throw new SurveyException(ErrorMessages.InputTextWithOutCommand);

            int commandLenght = command.Length;

            if (text.Length > commandLenght)
                TextWhitoutCommand = text.Substring(commandLenght, text.Length - commandLenght).TrimStart().TrimEnd();
        }

        /// <summary>
        ///     Represents a command from command list.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        ///     Represents a text without command.
        /// </summary>
        public string TextWhitoutCommand { get; set; }

        /// <summary>
        ///     Represents a full text.
        /// </summary>
        public string FullText { get; set; }
    }
}
