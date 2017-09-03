using System.Linq;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class ProfileConroller:ICommand
    {

        public ProfileConroller()
        {
            Name = CommandsList.CommandNewProfile;
        }

        [Dependency]
        public IProfile WorkingProfile { get; set; }

        /// <summary>
        ///     Method for writting results.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }


        /// <summary>
        ///     Name of ICommand implementations.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Interface ICommand implementations.
        /// </summary>
        public void Execute()
        {
            Initialize();
        }
        
        /// <summary>
        ///     Initialize ProfileController.
        /// </summary>
        private void Initialize()
        {
            m_CurrIndex = 0;
            m_CurrProfileItem = WorkingProfile.Items[0];
            Question(m_CurrProfileItem);
        }

        /// <summary>
        ///     Takes users answer.
        /// </summary>
        /// <param name="q">question</param>
        private void Question(ProfileItem q)
        {
            if(m_ProfileComplete)
                return;

          WriterAndReaderWorker.WriteLine(q.Question.QuestionText);
          System.Windows.Forms.SendKeys.SendWait(q.Answer);
          string userInput = WriterAndReaderWorker.ReadLine();
          CheckInput(userInput);
        }

        /// <summary>
        ///     Checks User input. Find command.
        /// </summary>
        /// <param name="userInput">User input</param>
        private void CheckInput(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                WriterAndReaderWorker.WriteLine(ErrorMessages.EmptyText);
            }
            else if (userInput == CommandsList.CommandProfileGoToPrevioseQuestion)
            {
                GoToPrevios();
            }
            else if (userInput == CommandsList.CommandProfileRestart)
            {
                ProfileRestart();
            }
            else if (userInput.Contains(CommandsList.CommandProfileGoToQuestion))
            {
                GoToTheQuestion(userInput);
            }
            else
            {
                CheckAnswer(userInput);
            }
        }

        /// <summary>
        ///     Checks answer. If corrtect - goes to the next question. 
        /// </summary>
        /// <param name="userInput">User input</param>
        private void CheckAnswer(string userInput)
        {
            try
            {
                m_CurrProfileItem.Answer = m_CurrProfileItem.Question.CheckedAnswer(userInput);

                if (WorkingProfile.Items.Any(n => n.Answer == ""))
                {
                    SetCurrIndex(WorkingProfile.Items.FindIndex(n => n.Answer == ""));
                }
                else
                {
                    m_ProfileComplete = true;
                }
            }
            catch (SurveyException QuestionExeption)
            {
                WriterAndReaderWorker.WriteLine(QuestionExeption.Message);
            }
            finally
            {
                if (m_ProfileComplete)
                {
                    WriterAndReaderWorker.WriteLine($"{SurveyConst.SaveProfile}{CommandsList.CommandSave}");
                    WriterAndReaderWorker.WriteLine($"{SurveyConst.StartMessages}");
                }
                else
                Question(m_CurrProfileItem);
            }
        }

        /// <summary>
        ///     Goes to the previos question.
        /// </summary>
        private void GoToPrevios()
        {
            if (m_CurrIndex > 0)
            {
                SetCurrIndex(m_CurrIndex--);
             
            }
            else
            {
                WriterAndReaderWorker.WriteLine(ErrorMessages.FirstElementWasReached);
            }

            Question(m_CurrProfileItem);
        }

        /// <summary>
        ///     Clears all answers.
        /// </summary>
        private void ProfileRestart()
        {
            foreach (var item in WorkingProfile.Items)
            {
                item.Answer = "";
            }

            m_CurrProfileItem = WorkingProfile.Items[0];
            m_CurrIndex = 0;

            Question(m_CurrProfileItem);
        }

        /// <summary>
        ///     Goes to the specified question.
        /// </summary>
        /// <param name="userInput">User input</param>
        private void GoToTheQuestion(string userInput)
        {

            int commandLenght = CommandsList.CommandProfileGoToQuestion.Length ;

            if (userInput.Length > commandLenght)
            {
                string indexString = userInput.Substring(commandLenght , userInput.Length - commandLenght).TrimStart().TrimEnd();
                int index;

                if (int.TryParse(indexString, out index))
                {
                    if (index < 1 || index > WorkingProfile.Items.Count)
                    {
                        string Error =
                            ErrorMessages.QuestionNumberMustBeBetweenMinAndMax.Replace("max",
                                WorkingProfile.Items.Count.ToString());
                        WriterAndReaderWorker.WriteLine(Error);
                    }
                }
                else
                {
                    WriterAndReaderWorker.WriteLine(ErrorMessages.QuestionNumberIsNotNumber);
                }

                SetCurrIndex(index-1);
                
                Question(m_CurrProfileItem);
            }
            else
            {
                WriterAndReaderWorker.WriteLine(ErrorMessages.QuestionNumberWasNotSpecified);
              
            }

            Question(m_CurrProfileItem);
        }

        /// <summary>
        ///     Sets question by index.
        /// </summary>
        /// <param name="i">index</param>
        private void SetCurrIndex(int i)
        {
            m_CurrIndex=i;
            m_CurrProfileItem = WorkingProfile.Items[i];
        }

       internal int m_CurrIndex;
       internal ProfileItem m_CurrProfileItem;
       internal bool m_ProfileComplete;
    }

}
