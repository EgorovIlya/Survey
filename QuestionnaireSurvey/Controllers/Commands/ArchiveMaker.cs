using System.IO;
using System.IO.Compression;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers.Commands
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
            m_UserInput = userInput;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents the userInput name.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        ///     Represents the path to the result directory.
        /// </summary>
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     ICommmand implementation. Crerates archive from specified profile.
        /// </summary>
        public void Execute()
        {
            CheckDir(PathToResults);

            string profileAndPathToSave = Tools.GetTextWithoutCommand(m_UserInput, CommandsList.CommandZip);
            string[] data = profileAndPathToSave.Split(' ');

            if(data.Length!=2)
                throw new SurveyException(ErrorMessages.ParametrsNotCorrect);

            string fullPath = Path.Combine(PathToResults, $"{data[0]}.txt");
            FileTxtExists(fullPath);

            string directoryWithResult = Path.GetDirectoryName(data[1]);

            if (!Directory.Exists(directoryWithResult))
                throw new SurveyException(ErrorMessages.ArchieveDirectoryNotExists);


           
            string filename = Path.GetFileName(fullPath);

            using (var dest = new FileStream(data[1], FileMode.Create))
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
        ///      Checks whether the directory exists.
        /// </summary>
        /// <param name="path"></param>
        private void CheckDir(string path)
        {
           if  (!Directory.Exists(path))
                throw new SurveyException( ErrorMessages.DirectoryNotExists);
        }

        /// <summary>
        ///     Checks whether the file exists.
        /// </summary>
        /// <param name="fullPath"></param>
        private void FileTxtExists(string fullPath)
        {
           if (!File.Exists(fullPath))
                throw new SurveyException($"{ErrorMessages.FileNotExists}.{fullPath}");
        }

        #endregion Private Methods

        /// <summary>
        ///     Users userInput.
        /// </summary>
        private string m_UserInput;
    }
}
