using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class Saver:ICommand
    {
        [Dependency]
        public IProfile WorkingProfile { get; set; }

        public Saver()
        {
            Name = CommandsList.CommandSave;
        }

        public string Name { get; }
        public string PathToResults { get; set; } = Path.Combine(Directory.GetCurrentDirectory(),SurveyConst.DirectoryName);

        public void Execute()
        {
            CheckProfile();
            Save();
        }

        public void Save()
        {
            if (!Directory.Exists(PathToResults))
            {
                Directory.CreateDirectory(PathToResults);
            }

            string [] answer = new string[WorkingProfile.Items.Count];

            for (int i=0; i<WorkingProfile.Items.Count();i++)
            {
                answer[i] = $"{i + 1}. {WorkingProfile.Items[i].Question.QuestionName}: {WorkingProfile.Items[i].Answer}";
            }
            string path = Path.Combine(PathToResults, $"{WorkingProfile.ProfileId}.txt");

            using (var writer = new StreamWriter(path))
            {
                foreach (var s in answer)
                {
                    writer.WriteLine(s, Environment.NewLine);
                }
            }
        }

        private void CheckProfile()
        {
            if (WorkingProfile.Items.Any(n => n.Answer == ""))
            {
                throw new SurveyException(ErrorMessages.ProfileIsNotCorrect);
            }
        }
        
    }
}
