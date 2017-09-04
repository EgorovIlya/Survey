using System;
using Survey.Interface;

namespace Survey.Controllers.Commands
{
    public class Exiter:ICommand
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Exiter.
        /// </summary>
        public Exiter()
        {
            CommandName = CommandsList.CommandExit;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Returns name of command.
        /// </summary>
        public string CommandName { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///      Exit from application.
        /// </summary>
        public void Execute()
        {
            Environment.Exit(0);
        }

        #endregion Public Methods
    }
}
