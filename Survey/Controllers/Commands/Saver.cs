using System;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    public class Saver:ICommand
    {
        #region Constructors

        /// <summary>
        ///      Initializes a new instance of the Saver.
        /// </summary>
        public Saver()
        {
            CommandName = CommandsList.CommandSave;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents a profile, that must be saved.
        /// </summary>
        [Dependency]
        public IProfile WorkingProfile { get; set; }

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
        ///     ICommand implementation. Saves the specified profile.
        /// </summary>
        public void Execute()
        {
            CheckProfile();
            Save();
        }

        #endregion Public Methods

        #region Private Methods

        private void Save()
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

        #endregion Private Methods

    }
}
