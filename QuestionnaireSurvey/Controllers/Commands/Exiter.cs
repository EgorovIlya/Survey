using System;
using QuestionnaireSurvey.Interface;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class Exiter:ICommand
    {
        #region Constructors

        public Exiter()
        {
            Name = CommandsList.CommandExit;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Returns name of command.
        /// </summary>
        public string Name { get; }

        #endregion Properties

        #region Public Methods

        public void Execute()
        {
            Environment.Exit(0);
        }

        #endregion Public Methods
    }
}
