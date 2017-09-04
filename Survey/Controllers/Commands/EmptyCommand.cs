using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    public class EmptyCommand:ICommand
    {
        #region Constructors

        public EmptyCommand()
        {
            CommandName = "";
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Returns name of command.
        /// </summary>
        public string CommandName { get; }

        #endregion Public Properties

        #region Public Methods

        public void Execute()
        {
          throw new SurveyException(ErrorMessages.CommandNotCorrect);
        }

        #endregion Public Methods
    }
}
