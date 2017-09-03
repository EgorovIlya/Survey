using System;

using QuestionnaireSurvey.Controllers.Commands;
using QuestionnaireSurvey.Interface;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.ModelsDTO;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers
{
    public class Controller
    {
        #region Constructors

        public Controller(IUnityContainer unityContainer)
        {
            m_UnityContainer = unityContainer;
        }

        #endregion Constructors

    #region Properties

    [Dependency]
        public ICommand Command { get; set; }

        #endregion Properties

        #region Public Methods

        public void Initialize()
        {
            Console.WriteLine($"{SurveyConst.StartMessages}");
            ReadLineAndSetCommadAndExecute();
        }

        /// <summary>
        ///     Sets command, if User input will be correct.
        /// </summary>
        /// <param name="userInput">User input.</param>
        public void SetCommand(string userInput)
        {
            if (userInput == CommandsList.CommandHelp)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandHelp);

            else if (userInput== CommandsList.CommandSave)
                Command = m_Profile!=null 
                    ? m_UnityContainer.Resolve<ICommand>(CommandsList.CommandSave,new ParameterOverride("WorkingProfile", m_Profile)) 
                    : m_UnityContainer.Resolve<ICommand>(CommandsList.CommandSave);
            else if (userInput.Contains(CommandsList.CommandNewProfile))
            {
                m_Profile = m_UnityContainer.Resolve<IProfile>();
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandNewProfile, new ParameterOverride("WorkingProfile", m_Profile));
            }

            else if (userInput == CommandsList.CommandExit)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandExit);

            else if (userInput == CommandsList.CommandStatistic)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandStatistic);

            else if (userInput.Contains(CommandsList.CommandFind))
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandFind
                                            , new ParameterOverride("command", userInput)
                                            , new ParameterOverride("name", CommandsList.CommandFind));

            else if (userInput.Contains(CommandsList.CommandDelete))
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandDelete
                                            ,new ParameterOverride("command", userInput)
                                            , new ParameterOverride("name", CommandsList.CommandDelete));

            else if (userInput == CommandsList.CommandList)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandList
                                            , new ParameterOverride("command", userInput)
                                            , new ParameterOverride("name", CommandsList.CommandList));


            else if (userInput == CommandsList.CommandListToday)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandListToday
                                            , new ParameterOverride("command", userInput)
                                            , new ParameterOverride("name", CommandsList.CommandListToday));

            else if (userInput == CommandsList.CommandZip)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandZip
                                            , new ParameterOverride("command", userInput));
            else
                Command = m_UnityContainer.Resolve<ICommand>();
        }

        private void Execute()
        {
            try
            {
                Command.Execute();
            }
            catch (SurveyException questionExeption)
            {
                Console.WriteLine(questionExeption.Message);
            }
            finally
            {
                ReadLineAndSetCommadAndExecute();
            }
        }

        /// <summary>
        ///     Reads line.
        /// </summary>
        private void ReadLineAndSetCommadAndExecute()
        {
            string userInput = Console.ReadLine();
            SetCommand(userInput);
            Execute();

        }

        #endregion Public Methods

        #region Fields

        IProfile m_Profile;
        private readonly IUnityContainer m_UnityContainer;
       
        #endregion Fields
       

    }
}
