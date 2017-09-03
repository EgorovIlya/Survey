using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class ArchiveMaker:ICommand
    {
        public ArchiveMaker()
        {
            Name = CommandsList.CommandZip;
        }

        public ArchiveMaker(string command)
        {
            Name = CommandsList.CommandZip;
            m_Command = command;
        }

        public string Name { get; }
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        /// <summary>
        ///     ICommmand implementation.
        /// </summary>
        public void Execute()
        {
            CheckDir(PathToResults);

            string profileAndPathToSave = Tools.GetTextWithoutCommand(m_Command, CommandsList.CommandZip);
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

        /// <summary>
        ///     Users command.
        /// </summary>
        private string m_Command;
    }
}
