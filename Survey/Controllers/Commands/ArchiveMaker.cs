﻿using System;
using System.IO;
using System.IO.Compression;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    public class ArchiveMaker:ICommand
    {
        #region Constructors
        /// <summary>
        ///      Initializes a new instance of the ArchiveMaker.
        /// </summary>
        public ArchiveMaker()
        {
            CommandName = CommandsList.CommandZip;
        }

        /// <summary>
        ///      Initializes a new instance of the ArchiveMaker using the assigned userInput.
        /// </summary>
        public ArchiveMaker(string userInput)
        {
            CommandName = CommandsList.CommandZip;
            m_UserInput = new UserInput(CommandsList.CommandZip, userInput); 
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents a command name.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        ///     Represents user input.
        /// </summary>
        public UserInput UserInput => m_UserInput;

        /// <summary>
        ///     Represents a path to the result directory.
        /// </summary>
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///    The ICommmand implementation. Creates archive from the specified profile.
        /// </summary>
        public void Execute()
        {
            CheckDir(PathToResults);

            string profileAndPathToSave = UserInput.TextWhitoutCommand;

            int idexOfprofileNameEnd = profileAndPathToSave.IndexOf(" ");

            if(idexOfprofileNameEnd==-1)
                throw new SurveyException(ErrorMessages.ParametrsNotCorrect);

            string profileName = profileAndPathToSave.Substring(0, idexOfprofileNameEnd);
            string pathToArchive = profileAndPathToSave.Substring(idexOfprofileNameEnd+1, profileAndPathToSave.Length - idexOfprofileNameEnd - 1);

            string fullPath = Path.Combine(PathToResults, $"{profileName}.txt");

            FileTxtExists(fullPath);

            string directoryWithResult = Path.GetDirectoryName(pathToArchive);

            if (!Directory.Exists(directoryWithResult))
                throw new SurveyException(ErrorMessages.ArchieveDirectoryNotExists);

           
            string filename = Path.GetFileName(fullPath);

            using (var dest = new FileStream(pathToArchive, FileMode.Create))
            using (var archive = new ZipArchive(dest, ZipArchiveMode.Create))
            using (var source = new FileStream(fullPath, FileMode.Open))
            {
                ZipArchiveEntry entry = archive.CreateEntry(filename);
                byte[] bytes = new byte[source.Length];
                source.Read(bytes, 0, bytes.Length);
                using (var stream = entry.Open())
                    stream.Write(bytes, 0, bytes.Length);
            }

        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///      Checks if the directory exists.
        /// </summary>
        /// <param name="path"></param>
        private void CheckDir(string path)
        {
           if  (!Directory.Exists(path))
                throw new SurveyException( ErrorMessages.DirectoryNotExists);
        }

        /// <summary>
        ///     Checks if the file exists.
        /// </summary>
        /// <param name="fullPath"></param>
        private void FileTxtExists(string fullPath)
        {
           if (!File.Exists(fullPath))
                throw new SurveyException($"{ErrorMessages.FileNotExists}.{fullPath}");
        }

        #endregion Private Methods

        /// <summary>
        ///     Users input.
        /// </summary>
        private UserInput m_UserInput;
    }
}
