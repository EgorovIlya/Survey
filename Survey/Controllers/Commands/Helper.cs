using Microsoft.Practices.Unity;
using Survey.Interface;

namespace Survey.Controllers.Commands
{
    /// <summary>
    ///     Represents a Helper that shows a list of commands with description.
    /// </summary>
    public class Helper : ICommand
    {
        #region Constructors

        /// <summary>
        ///      Initializes a new instance of the Helper.
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
        ///   Represents a method for writing results and reads user input.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     ICommand implementation. Displays help.
        /// </summary>
        public void Execute()
        {
            ShowHelp();
        }
        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Displays list of the commands.
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
