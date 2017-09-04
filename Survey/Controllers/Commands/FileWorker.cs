using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    /// <summary>
    ///     Represents a class that can works with saved profile.
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
            m_UserInput = userInput;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents a method for writting results and reads user input.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        /// <summary>
        ///     Represents a command from commandList.
        /// </summary>
        public string CommandName { get; set; } 

        /// <summary>
        ///     Represnts a path to the result directory. 
        /// </summary>
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Finds saved profiles, shows depending on the entered command
        /// </summary>
        public void Execute()
        {
            if (!CheckDir()) return;
            
            if (m_UserInput.Contains(CommandsList.CommandFind) || m_UserInput.Contains(CommandsList.CommandDelete))
            {
                string fileName = Tools.GetTextWithoutCommand(m_UserInput, CommandName);
                string fullPath = Path.Combine(PathToResults, $"{fileName}.txt");

                if (!FileTxtExists(fullPath)) return;

                if(m_UserInput.Contains(CommandsList.CommandFind))
                      VeiwProfile(fullPath);

                if (m_UserInput.Contains(CommandsList.CommandDelete))
                    File.Delete(fullPath);
            }
            else if (m_UserInput==CommandsList.CommandList  || m_UserInput==CommandsList.CommandListToday)
            {
                GetFiles(PathToResults);

                if (m_UserInput==CommandsList.CommandList)
                    GetAllSavedProfile();
                else if (m_UserInput==CommandsList.CommandListToday)
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
        private bool FileTxtExists(string fullPath)
        {
           var result = File.Exists(fullPath);
            if (!result)
                throw new SurveyException(ErrorMessages.FileNotExists);
            return result;
        }

        /// <summary>
        ///     Reads lines form file, and shows them in the console.
        /// </summary>
        /// <param name="fullPath">path to the profile</param>
        private void VeiwProfile(string fullPath)
        {
            using (StreamReader r = new StreamReader(fullPath,  true))
            {
                string srline;
                while ((srline = r.ReadLine()) != null)
                {
                    WriterAndReaderWorker.WriteLine(srline);
                }
            }
        }

        /// <summary>
        ///     Checks if directory exists.
        /// </summary>
        /// <returns></returns>
        private bool CheckDir()
        {
            var result = Directory.Exists(PathToResults);
            if (!result)
               throw new SurveyException(ErrorMessages.DirectoryNotExists);
            return result;
        }

        /// <summary>
        ///     Gets list of files in the result directory.
        /// </summary>
        private void GetAllSavedProfile()
        {
            foreach (var file in m_files)
            {
                WriterAndReaderWorker.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }

        /// <summary>
        ///     Gets profiles, that was saved today.
        /// </summary>
        private void GetTodaySavedProfile()
        {
            foreach (var file in m_files)
            {
                DateTime dateCreation = File.GetCreationTime(file);
                if (dateCreation >= DateTime.Today 
                    && dateCreation < DateTime.Today.AddDays(1))
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
        ///     The user input.
        /// </summary>
        private readonly string m_UserInput;

        #endregion Private Properties
    }
}
