using System;
using QuestionnaireSurvey.Controllers.Commands;
using QuestionnaireSurvey.Interface;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers
{
    public class Controller
    {

        #region Constructors

        /// <summary>
        ///      Initializes a new instance of the Controller using the specified IUnityContainer.
        /// </summary>
        /// <param name="unityContainer">the specified IUnityContainer</param>
        public Controller(IUnityContainer unityContainer)
        {
            m_UnityContainer = unityContainer;
        }

        #endregion Constructors

        #region Public Properties

        [Dependency]
        public ICommand Command { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Initialize()
        {
            Console.WriteLine($"{SurveyConst.StartMessages}");
            ReadLineAndSetCommadAndExecute();
        }

        /// <summary>
        ///     Sets command, if User input is correct.
        /// </summary>
        /// <param name="userInput">User input.</param>
        public void SetCommand(string userInput)
        {
            if (userInput == CommandsList.CommandHelp)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandHelp);

            else if (userInput== CommandsList.CommandSave)
                Command = m_Profile!=null 
                    ? m_UnityContainer.Resolve<ICommand>(CommandsList.CommandSave,new ParameterOverride(SurveyConst.WorkingProfile, m_Profile)) 
                    : m_UnityContainer.Resolve<ICommand>(CommandsList.CommandSave);
            else if (userInput.Contains(CommandsList.CommandNewProfile))
            {
                m_Profile = m_UnityContainer.Resolve<IProfile>();
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandNewProfile, new ParameterOverride(SurveyConst.WorkingProfile, m_Profile));
            }

            else if (userInput == CommandsList.CommandExit)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandExit);

            else if (userInput == CommandsList.CommandStatistic)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandStatistic);

            else if (userInput.Contains(CommandsList.CommandFind))
                Command = ResolveFileWorker(CommandsList.CommandFind,userInput);

            else if (userInput.Contains(CommandsList.CommandDelete))
                Command = ResolveFileWorker(CommandsList.CommandDelete, userInput);
           
            else if (userInput == CommandsList.CommandList)
                Command = ResolveFileWorker(CommandsList.CommandList, userInput);

            else if (userInput == CommandsList.CommandListToday)
                Command = ResolveFileWorker(CommandsList.CommandListToday, userInput);

            else if (userInput == CommandsList.CommandZip)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandZip
                                            , new ParameterOverride(SurveyConst.UserInput, userInput));
            else
                Command = m_UnityContainer.Resolve<ICommand>();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Executes assigned  command.
        /// </summary>
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

        /// <summary>
        ///     Returns  implementation of FileWorker by using constructor with specified commandName and userInput.
        /// </summary>
        /// <param name="commandName">the name of the ommand</param>
        /// <param name="userInput">user input</param>
        /// <returns></returns>
        private ICommand ResolveFileWorker(string commandName , string userInput)
        {
            return m_UnityContainer.Resolve<ICommand>(commandName
                , new ParameterOverride(SurveyConst.UserInput, userInput)
                , new ParameterOverride(SurveyConst.CommandName, commandName));
        }

        #endregion Private Methods

        #region Private Properties
        /// <summary>
        ///     
        /// </summary>
        private IProfile m_Profile;

        /// <summary>
        ///     
        /// </summary>
        private readonly IUnityContainer m_UnityContainer;

        #endregion Private Properties


    }
}
