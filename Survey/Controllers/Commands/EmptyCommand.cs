using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    public class EmptyCommand:ICommand
    {
        #region Constructors

        /// <summary>
        ///      Initializes a new instance of the EmptyCommand.
        /// </summary>
        public EmptyCommand()
        {
            CommandName = "";
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Returns the name of  the command.
        /// </summary>
        public string CommandName { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///      The ICommmand implementation. Throws exception.
        /// </summary>
        public void Execute()
        {
          throw new SurveyException(ErrorMessages.CommandNotCorrect);
        }

        #endregion Public Methods
    }
}
