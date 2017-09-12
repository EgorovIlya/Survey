using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    /// <summary>
    ///     Represents a class which can works with saved profile.
    /// </summary>
    public class FileWorker:ICommand
    {
        #region Constructors

        /// <summary>
        ///      Initializes a new instance of the FileWorker using the specified command name.
        /// </summary>
        /// <param name="commandName">element of command list</param>
        public FileWorker(string commandName)
        {
            CommandName = commandName;
        }

        /// <summary>
        ///      Initializes a new instance of the FileWorker  using the specified command name and full command.
        /// </summary>
        /// <param name="userInput">user input</param>
        /// <param name="commandName">element of command list</param>
        [InjectionConstructor]
        public FileWorker(string userInput,string commandName)
        {
            CommandName = commandName;
            m_UserInput = new UserInput(commandName,userInput);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents a method for writing results and reading user input.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        /// <summary>
        ///     Represents a command from commandList.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        ///     Represents a user input.
        /// </summary>
        public UserInput UserInput => m_UserInput;

        /// <summary>
        ///     Represents a path to the result directory. 
        /// </summary>
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///      The ICommmand implementation. Finds saved profiles, shows result depending on the entered command.
        /// </summary>
        public void Execute()
        {
            CheckDir();
            
            if (m_UserInput.CommandName == CommandsList.CommandFind)
            {
                string fullPath = CheckTextWithoutCommandAndReturnFullPath();
                VeiwProfile(fullPath);
            }

            else if (m_UserInput.CommandName == CommandsList.CommandDelete)
            {
                string fullPath =CheckTextWithoutCommandAndReturnFullPath();
                File.Delete(fullPath);
            }
        
            else if (m_UserInput.FullText == CommandsList.CommandList)
            {
                GetFiles(PathToResults);
                GetAllSavedProfile();
            }
            else if (m_UserInput.FullText == CommandsList.CommandListToday)
            {
                GetFiles(PathToResults);
                GetTodaySavedProfile();
            }
            else
            {
                throw new SurveyException(ErrorMessages.CommandNotCorrect);
            }
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        ///     Checks if the file exists.
        /// </summary>
        /// <param name="fullPath">path to the profile</param>
        /// <returns></returns>
        private void FileTxtExists(string fullPath)
        {
           var result = File.Exists(fullPath);
           if (!result)
                throw new SurveyException(ErrorMessages.FileNotExists);
        }


        /// <summary>
        ///     Checks 
        /// </summary>
        /// <returns></returns>
        private string  CheckTextWithoutCommandAndReturnFullPath()
        {
            if (string.IsNullOrEmpty(m_UserInput.TextWhitoutCommand))
                throw new SurveyException(ErrorMessages.CommandMustBeWithParametrs);

            string fullPath = FullPath();

            FileTxtExists(fullPath);

            return fullPath;
        }

        private string FullPath()
        {
            return Path.Combine(PathToResults, $"{m_UserInput.TextWhitoutCommand}.txt");
        }

        /// <summary>
        ///     Reads lines form a file, and shows them in the console.
        /// </summary>
        /// <param name="fullPath">path to the profile</param>
        private void VeiwProfile(string fullPath)
        {
            SavedProfileReader savedProfile = new SavedProfileReader(fullPath);

            if (savedProfile.ProfileStrings.Any())
            {
                foreach (var str in savedProfile.ProfileStrings)
                {
                    WriterAndReaderWorker.WriteLine(str);
                }
            }
        }

        /// <summary>
        ///     Checks if the directory exists.
        /// </summary>
        /// <returns></returns>
        private void CheckDir()
        {
            var result = Directory.Exists(PathToResults);
            if (!result)
               throw new SurveyException(ErrorMessages.DirectoryNotExists);
        }

        /// <summary>
        ///     Gets the list of files in the result directory.
        /// </summary>
        private void GetAllSavedProfile()
        {
            foreach (var file in m_files)
            {
                WriterAndReaderWorker.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }

        /// <summary>
        ///     Gets the profiles, that was saved today.
        /// </summary>
        private void GetTodaySavedProfile()
        {
            foreach (var file in m_files)
            {
                SavedProfileReader savedProfile = new SavedProfileReader(file);

                if (savedProfile.DateCreation >= DateTime.Today
                                    && savedProfile.DateCreation < DateTime.Today.AddDays(1))
                    WriterAndReaderWorker.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }

        /// <summary>
        ///     Gets all saved profiles.
        /// </summary>
        private void GetFiles(string path)
        {
            m_files = Directory.GetFiles(path).OrderBy(n=>n).ToList();
        }

        #endregion Private Methods

        #region Private Properties

        /// <summary>
        ///     The list of the files.
        /// </summary>
        private List<string> m_files;

        /// <summary>
        ///    User input.
        /// </summary>
        private readonly UserInput m_UserInput;

        #endregion Private Properties
    }
}
