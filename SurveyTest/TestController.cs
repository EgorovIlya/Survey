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
        ///     Checks the actions when the "Help" command is entered.
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
        ///     Checks the actions when the "Save" command is entered.
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
        ///      Checks the actions when the "new_profile" command is entered.
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
        ///  Checks the actions when the "delete" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandDeleteReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandDelete;
            string userInput = $"{command} 123-123-123-1";
            var controller = m_container.Resolve<Controller>();
            
            //Act
            controller.SetCommand(userInput);

            FileWorker fileWorker = controller.Command as FileWorker;

            Assert.AreNotEqual(null, fileWorker,"The instance is not correct");
            Assert.AreEqual(userInput, fileWorker.UserInput, "User input is not correct");
        }

        /// <summary>
        ///     Checks the actions when the "List" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandListReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandList;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            FileWorker fileWorker = controller.Command as FileWorker;

            //Assert
            Assert.AreNotEqual(null, fileWorker, "The instance is not correct");
            Assert.AreEqual(command, fileWorker.UserInput, "User input is not correct");
        }

        /// <summary>
        ///     Checks the actions when the "Find" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandFindReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandFind;
            string userInput = $"{command} 123-123-123-1";
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(userInput);

            FileWorker fileWorker = controller.Command as FileWorker;

            Assert.AreNotEqual(null, fileWorker, "The instance is not correct");
            Assert.AreEqual(userInput, fileWorker.UserInput, "User input is not correct");
        }

        /// <summary>
        ///      Checks the actions when the "Zip" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandArchiveReturnArchiveMaker()
        {
            //Arrange
            string command = CommandsList.CommandZip;
            string userInput = $"{command} 123-123-123-1 C:\\Arcive.zip";
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(userInput);

            ArchiveMaker archiveMaker = controller.Command as ArchiveMaker;

            Assert.AreNotEqual(null, archiveMaker, "The instance is not correct");
            Assert.AreEqual(userInput, archiveMaker.UserInput, "User input is not correct");
        }

        /// <summary>
        ///     Checks the actions when the "Exit" command is entered. 
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
        ///     Checks the actions when the "ListToday" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandListTodayReturnFileWorker()
        {
            //Arrange
            string command = CommandsList.CommandListToday;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            FileWorker fileWorker = controller.Command as FileWorker;

            //Assert
            Assert.AreNotEqual(null, fileWorker, "The instance is not correct");
            Assert.AreEqual(command, fileWorker.UserInput, "User input is not correct");
        }

        /// <summary>
        ///      Checks the actions when the "Statistics" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandStatisticsReturnStatisticMaker()
        {
            //Arrange
            string command = CommandsList.CommandStatistics;
            var controller = m_container.Resolve<Controller>();
            //Act
            controller.SetCommand(command);
            Type typeWasSet = controller.Command.GetType();
            Type typeMustSet = m_container.Resolve<ICommand>(CommandsList.CommandStatistics).GetType();

            //Assert
            Assert.AreEqual(typeMustSet, typeWasSet);
        }

        /// <summary>
        ///     Checks the actions when the "Empty" command  is entered.
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
        ///     Checks when the incorret string is inputted. 
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
        ///     Checks the commands dictionary. 
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
