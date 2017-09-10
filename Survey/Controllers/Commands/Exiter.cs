using System;
using Survey.Interface;

namespace Survey.Controllers.Commands
{
    /// <summary>
    ///         Represents a class that closes an application.
    /// </summary>
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
        ///     Represents a command from commandList.
        /// </summary>
        public string CommandName { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///      Exits from application.
        /// </summary>
        public void Execute()
        {
            Environment.Exit(0);
        }

        #endregion Public Methods
    }
}
