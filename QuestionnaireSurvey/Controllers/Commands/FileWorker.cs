using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class FileWorker:ICommand
    {
       
        public FileWorker(string name)
        {
            Name = name;
        }

        [InjectionConstructor]
        public FileWorker(string command,string name)
        {
            Name = name;
            m_Command = command;
        }

        /// <summary>
        ///     Method for writting results.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }
        public string Name { get; set; } 
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        public void Execute()
        {
            if (!CheckDir()) return;
            
            if (m_Command.Contains(CommandsList.CommandFind) || m_Command.Contains(CommandsList.CommandDelete))
            {
                string fileName = Tools.GetTextWithoutCommand(m_Command, Name);
                string fullPath = Path.Combine(PathToResults, $"{fileName}.txt");

                if (!FileTxtExists(fullPath)) return;

                if(m_Command.Contains(CommandsList.CommandFind))
                      VeiwProfile(fullPath);

                if (m_Command.Contains(CommandsList.CommandDelete))
                    File.Delete(fullPath);
            }
            else if (m_Command==CommandsList.CommandList  || m_Command==CommandsList.CommandListToday)
            {
                GetFiles(PathToResults);

                if (m_Command==CommandsList.CommandList)
                    GetAllSavedProfile();
                else if (m_Command==CommandsList.CommandListToday)
                    GetTodaySavedProfile();
            }
            else
            {
                throw new SurveyException(ErrorMessages.CommandNotCorrect);
            }
        }

        private bool FileTxtExists(string fullPath)
        {
           var result = File.Exists(fullPath);
            if (!result)
                throw new SurveyException(ErrorMessages.FileNotExists);
            return result;
        }

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

        private bool CheckDir()
        {
            var result = Directory.Exists(PathToResults);
            if (!result)
               throw new SurveyException(ErrorMessages.DirectoryNotExists);
            return result;
        }

        private void GetAllSavedProfile()
        {
            foreach (var file in m_files)
            {
                WriterAndReaderWorker.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }

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

        private void GetFiles(string path)
        {
            m_files = Directory.GetFiles(path).OrderBy(n=>n).ToList();
        }

        private  List<string> m_files;

        private string m_Command;
    }
}
