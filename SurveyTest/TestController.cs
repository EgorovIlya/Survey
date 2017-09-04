using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Survey;
using Survey.Controllers;
using Survey.Controllers.Commands;
using Survey.Interface;
using Survey.Utils;

namespace SurveyTest
{
    [TestFixture]
    public class TestController
    {
        /// <summary>
        ///     Initializes beginning data before all tests are run.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);
        }

        /// <summary>
        ///     Checks if command "help" was inputted.  
        /// </summary>
        [Test]
        public void TestSetCommandHelpReturnHelper()
        {
           //Arrange
            string command =CommandsList.CommandHelp;
            var controller = m_container.Resolve<Controller>();

            //Act
            controller.SetCommand(command);
            Type typeWasSet= controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandHelp).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "save" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandSaveReturnSaver()
        {
            //Arrange
            string command = CommandsList.CommandSave;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandSave).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "new profile" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandNewProfileReturnProfileController()
        {
            //Arrange
            string command = CommandsList.CommandNewProfile;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandNewProfile).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "delete" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandDeleteReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandDelete;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandDelete 
                , new ParameterOverride(SurveyConst.UserInput, command)
                , new ParameterOverride(SurveyConst.CommandName, command)).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "List" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandListReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandList;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandList
                , new ParameterOverride(SurveyConst.UserInput, command)
                , new ParameterOverride(SurveyConst.CommandName, command)).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "find" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandFindReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandFind;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandFind
                    ,new ParameterOverride(SurveyConst.UserInput, command)
                    , new ParameterOverride(SurveyConst.CommandName, command))
                .GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "exit" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandExitReturnExiter()
        {
            //Arrange
            string command = CommandsList.CommandExit;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandExit).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "list today" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandListTodayReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandListToday;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandListToday
                    , new ParameterOverride(SurveyConst.UserInput, command)
                    , new ParameterOverride(SurveyConst.CommandName, command))
                .GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if command "statistic" was inputted. 
        /// </summary>
        [Test]
        public void TestSetCommandStatisticReturnStatisticMaker()
        {
            //Arrange
            string command = CommandsList.CommandStatistic;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandStatistic).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if empty was inputted. 
        /// </summary>
        [Test]
        public void TestSetEmptyReturnEmptyCommand()
        {
            //Arrange
            string command ="";
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>().GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks if incorret string was inputted. 
        /// </summary>
        [Test]
        public void TestSetIncorrestInputReturnEmptyCommand()
        {
            //Arrange
            string command = "asdf";
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>().GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks commands dictionary. 
        /// </summary>
        [Test]
        public void TestSetFindForgettingCommandsReturnForgettingCommands()
        {
            //Arrange
            var controllerCommads = CommandsList.CommandsListDict.Where(
                v => v.Key != CommandsList.CommandProfileGoToQuestion
                     && v.Key != CommandsList.CommandProfileGoToPrevioseQuestion
                     && v.Key != CommandsList.CommandProfileRestart);

            List<string> forgettingCommands = new List<string>();


            //Act
            foreach (var command in controllerCommads)
            {
                var controller = m_container.Resolve<Controller>();
                controller.SetCommand(command.Key);
               
                if(controller.Command.GetType() == typeof(EmptyCommand))
                    forgettingCommands.Add(command.Key);
            }

            //Assert
            Assert.AreEqual(0, forgettingCommands.Count);
        }

        private IUnityContainer m_container;
    }
}
