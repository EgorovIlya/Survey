using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using QuestionnaireSurvey;
using QuestionnaireSurvey.Controllers.Commands;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;
using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;
using QuestionnaireSurvey.Utils;


namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestArchiveMaker class.
    /// </summary>
    [TestFixture]
    public class TestArchiveMaker
    {
        /// <summary>
        ///     Initializes beginning data before all tests are run.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);

            m_profile1Mock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО", 0,300), "Рэй Бредбери"),

            });
            m_profile1Mock.Setup(a => a.ProfileId).Returns("123-123-123456-1");

            m_IfDirCreate = Directory.Exists(SurveyConst.DirectoryName);
            if (!Directory.Exists(TestArchiveDirectoryName))
                Directory.CreateDirectory(TestArchiveDirectoryName);
            m_fullPathToDir = Path.GetFullPath(TestArchiveDirectoryName);
            m_fullPathToArchive = Path.Combine(m_fullPathToDir, "testArchive.zip");
            pathToProfile1Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile1Mock.Object.ProfileId}.txt");

        }

        /// <summary>
        ///     Initializes beginning data before each test is run.
        /// </summary>
        [SetUp]
        public void Start()
        {
            var saver1 = m_container.Resolve<ICommand>(CommandsList.CommandSave
                , new PropertyOverride("WorkingProfile", m_profile1Mock.Object));

            Saver saver = saver1 as Saver;
            saver.Execute();
        }

        /// <summary>
        ///     Cleans test data after each test is run.
        /// </summary>
        [OneTimeTearDown]
        public void CleanUp()
        {
            if (!m_IfDirCreate && Directory.Exists(SurveyConst.DirectoryName))
                Directory.Delete(SurveyConst.DirectoryName, true);
            else
            {
                if (File.Exists(pathToProfile1Mock))
                    File.Delete(pathToProfile1Mock);
                Console.WriteLine("All Clear!");
            }

            Directory.Delete(m_fullPathToDir, true);
        }


        /// <summary>
        ///     Checks that an exception  will be thrown when params is not correct.  
        /// </summary>
        [Test]
        public void TestIfPararmsNotCorretReturnException()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandZip} wrongFrofileName1 wrongFrofileName2 {m_fullPathToArchive}";

            //Act
            GetArchiveMaker(commandText);

            //Assert
            Assert.That(() => { m_ArchiveMaker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.ParametrsNotCorrect));
        }

        /// <summary>
        ///      Checks that an exception will be thrown when profile does not exists.   
        /// </summary>
        [Test]
        public void TestIfProfileNotRxistsReturnException()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandZip} wrongFrofileName {m_fullPathToArchive}";

            //Act
            GetArchiveMaker(commandText);

            //Assert
            Assert.That(() => { m_ArchiveMaker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contains(ErrorMessages.FileNotExists));
        }

        /// <summary>
        ///      Checks that an exception will be thrown when archive directory  does not exists.   
        /// </summary>
        [Test]
        public void TestIfPathForFindFileIsEmptyReturnException()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandZip}  {m_profile1Mock.Object.ProfileId} asdf";

            //Act
            GetArchiveMaker(commandText);

            //Assert
            Assert.That(() => { m_ArchiveMaker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.ArchieveDirectoryNotExists));
        }


         /// <summary>
        ///      Checks that the archive file is created.   
        /// </summary>
        [Test]
        public void TestThatArchiveIsCreated()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandZip}  {m_profile1Mock.Object.ProfileId} {m_fullPathToArchive}";
          
            //Act
            GetArchiveMaker(commandText);

            //Assert
            Assert.IsFalse(File.Exists(m_fullPathToArchive),$"Файл уже создан, до выполнения теста! {m_fullPathToArchive}");
            m_ArchiveMaker.Execute();
            Assert.IsTrue(File.Exists(m_fullPathToArchive), $"Файл архива не был создан!!! {m_fullPathToArchive}");
        }

        private void GetArchiveMaker(string input)
        {
            var amICommand = m_container.Resolve<ICommand>(CommandsList.CommandZip
                , new ParameterOverride("command", input)
                );

            m_ArchiveMaker = amICommand as ArchiveMaker;
            Assert.AreNotEqual(null, m_ArchiveMaker, "Ошибка при DI. Проверить настройки UNITY");
        }

        private string pathToProfile1Mock;
        Mock<IProfile> m_profile1Mock = new Mock<IProfile>();
        private bool m_IfDirCreate;
        private IUnityContainer m_container;
        private ArchiveMaker m_ArchiveMaker;
        private const string TestArchiveDirectoryName = "TestArchiveMakerDirectory";
        private string m_fullPathToDir;
        private string m_fullPathToArchive;
    }
}
