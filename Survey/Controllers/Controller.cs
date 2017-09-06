using System;
using Microsoft.Practices.Unity;
using Survey.Controllers.Commands;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers
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

        /// <summary>
        ///     Represents an ICommand object.
        /// </summary>
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
        ///     Sets the command, if user input is correct.
        /// </summary>
        /// <param name="userInput">User input.</param>
        public void SetCommand(string userInput)
        {
            if (userInput == CommandsList.CommandHelp)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandHelp);

            else if (userInput== CommandsList.CommandSave)
                Command = m_Profile!=null 
                    ? m_UnityContainer.Resolve<ICommand>(CommandsList.CommandSave, new PropertyOverride(SurveyConst.WorkingProfile, m_Profile)) 
                    : m_UnityContainer.Resolve<ICommand>(CommandsList.CommandSave);
            else if (userInput.Contains(CommandsList.CommandNewProfile))
            {
                m_Profile = m_UnityContainer.Resolve<IProfile>();
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandNewProfile, new PropertyOverride(SurveyConst.WorkingProfile, m_Profile));
            }
            else if (userInput == CommandsList.CommandExit)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandExit);

            else if (userInput == CommandsList.CommandStatistics)
                Command = m_UnityContainer.Resolve<ICommand>(CommandsList.CommandStatistics);

            else if (userInput.Contains(CommandsList.CommandFind))
                Command = Resolve(CommandsList.CommandFind,userInput);

            else if (userInput.Contains(CommandsList.CommandDelete))
                Command = Resolve(CommandsList.CommandDelete, userInput);
           
            else if (userInput == CommandsList.CommandList)
                Command = Resolve(CommandsList.CommandList, userInput);

            else if (userInput == CommandsList.CommandListToday)
                Command = Resolve(CommandsList.CommandListToday, userInput);

            else if (userInput.Contains(CommandsList.CommandZip))
                Command = Resolve(CommandsList.CommandZip, userInput);
            else
                Command = m_UnityContainer.Resolve<ICommand>();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Executes the assigned command.
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
        ///     Returns the ICommand implementation using the constructor with the specified command name and user input.
        /// </summary>
        /// <param name="commandName">the name of the command</param>
        /// <param name="userInput">user input</param>
        /// <returns></returns>
        private ICommand Resolve(string commandName , string userInput)
        {
            return m_UnityContainer.Resolve<ICommand>(commandName
                , new ParameterOverride(SurveyConst.UserInput, userInput)
                , new ParameterOverride(SurveyConst.CommandName, commandName));
        }

        #endregion Private Methods

        #region Private Properties
        /// <summary>
        ///    The current profile.
        /// </summary>
        private IProfile m_Profile;

        /// <summary>
        ///     The unity container.
        /// </summary>
        private readonly IUnityContainer m_UnityContainer;

        #endregion Private Properties


    }
}
