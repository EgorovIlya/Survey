using System;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Interface;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class Helper : ICommand
    {

        #region Constructors


        public Helper()
        {
            Name = CommandsList.CommandHelp;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Returns name of command.
        /// </summary>
        public string Name { get; }


        /// <summary>
        ///     Method for writting results.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        #endregion Properties

        #region Public Methods

        public void Execute()
        {
            ShowHelp();
        }

        /// <summary>
        ///     Shows list of the commands.
        /// </summary>
        private void ShowHelp()
        {
            foreach (var item in CommandsList.CommandsListDict)
            {
                WriterAndReaderWorker.WriteLine($"{item.Key} {item.Value}");
            }
        }

        #endregion Public Methods


    }
}
