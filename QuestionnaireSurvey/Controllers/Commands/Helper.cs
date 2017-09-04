﻿using System;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Interface;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class Helper : ICommand
    {

        #region Constructors
        /// <summary>
        ///      Initializes a new instance of the Saver.
        /// </summary>
        public Helper()
        {
            CommandName = CommandsList.CommandHelp;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents a name of command.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        ///   Represents a method for writting results and reads user input.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     ICommand implementation.
        /// </summary>
        public void Execute()
        {
            ShowHelp();
        }
        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods

    }
}
