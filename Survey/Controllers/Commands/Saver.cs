using System;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    /// <summary>
    ///     Represents a class that can saves the profile.
    /// </summary>
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
        ///     Represents a method for writing results and reading user input.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        /// <summary>
        ///     Represents the profile that needs to be saved.
        /// </summary>
        [Dependency]
        public IProfile WorkingProfile { get; set;}

        /// <summary>
        ///     Represents a command from command list.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        ///     Represents a path to the result directory. 
        /// </summary>
        public string PathToResults { get; set; } = SurveyConst.DirectoryName;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     The ICommand implementation. Saves the specified profile.
        /// </summary>
        public void Execute()
        {
            CheckProfile();
            Save();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Saves the specified profile.
        /// </summary>
        private void Save()
        {
            if (!Directory.Exists(PathToResults))
            {
                Directory.CreateDirectory(PathToResults);
            }

            string [] answer = new string[WorkingProfile.Items.Count];

            for (int i=0; i<WorkingProfile.Items.Count();i++)
            {
                answer[i] = $"{i + 1}. {WorkingProfile.Items[i].Question.QuestionName}{SurveyConst.Separator}{WorkingProfile.Items[i].Answer}";
            }

            string path = Path.Combine(PathToResults, $"{WorkingProfile.ProfileId}.txt");

            using (var writer = new StreamWriter(path))
            {
                foreach (var s in answer)
                {
                    writer.WriteLine(s, Environment.NewLine);
                }

                writer.WriteLine($"{SurveyConst.ProfileWasCreated}{SurveyConst.Separator}{DateTime.Now.ToString(SurveyConst.FormatDate)}", Environment.NewLine);
            }

            WriterAndReaderWorker.WriteLine($"{SurveyConst.FileWasSavedByPath} {Path.GetFullPath(path)}");
        }

        /// <summary>
        ///     Checks the specified profile.
        /// </summary>
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
