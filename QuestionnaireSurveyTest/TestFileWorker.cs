using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using QuestionnaireSurvey;
using QuestionnaireSurvey.Controllers.Commands;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;
using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;
using QuestionnaireSurvey.Utils;
using System;
using System.Collections.Generic;
using System.IO;


namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestFileWorker class.
    /// </summary>
    [TestFixture]
    public class TestFileWorker
    {
        /// <summary>
        ///     Initializes beginning data before all tests are run.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            m_OutputMessages = new List<string>();
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);
            m_Writer.Setup(c => c.WriteLine(It.IsAny<string>())).Callback((string s) => m_OutputMessages.Add(s));

            m_IfDirCreate = Directory.Exists(SurveyConst.DirectoryName);

            m_profile1Mock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300), "Рэй Бредбери"),

            });
            m_profile1Mock.Setup(a => a.ProfileId).Returns("123-123-123456-1");

            m_profile2Mock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300) , "Айзек Азимов"),

                new ProfileItem( new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
                    , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan)  ,DateTime.Now) ,"10.10.1922"),

            });
            m_profile2Mock.Setup(a => a.ProfileId).Returns("123-123-123456-2");
           

            m_profile3Mock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300) , "Буджолд МакМастер"),

                new ProfileItem( new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
                    , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan)  ,DateTime.Now),"22.11.1989"),

            });
            m_profile3Mock.Setup(a => a.ProfileId).Returns("123-123-123456-3");
          

            pathToProfile1Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile1Mock.Object.ProfileId}.txt");

            pathToProfile2Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile2Mock.Object.ProfileId}.txt");

            pathToProfile3Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile3Mock.Object.ProfileId}.txt");
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
            saver.WorkingProfile = m_profile2Mock.Object;
            saver.Execute();
            saver.WorkingProfile = m_profile3Mock.Object;
            saver.Execute();

            m_ExpectedMessage = new List<string>();
            m_OutputMessages.Clear();
        }

        /// <summary>
        ///     Cleans test data after each test is run.
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
            if (!m_IfDirCreate && Directory.Exists(SurveyConst.DirectoryName))
                Directory.Delete(SurveyConst.DirectoryName, true);
            else
            {
                if (File.Exists(pathToProfile1Mock))
                    File.Delete(pathToProfile1Mock);

                if (File.Exists(pathToProfile2Mock))
                    File.Delete(pathToProfile2Mock);

                if (File.Exists(pathToProfile3Mock))
                    File.Delete(pathToProfile3Mock);
                Console.WriteLine("All Clear!");
            }
        }

        /// <summary>
        ///     Checks that the exception will be thrown when try to delete file by wrong path.  
        /// </summary>
        [Test]
        public void TestWrongPathForDeleteFileReturnExcetion()
        {
            //Arrange
           string commandText = $"{CommandsList.CommandDelete} asdf";

            //Act
            GetFileWorker(commandText, CommandsList.CommandDelete);

            //Assert
            Assert.That(() => { m_FileWorker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.FileNotExists));
        }

        /// <summary>
        ///     Checks that the exception will be thrown when try to delete file by empty path.  
        /// </summary>
        [Test]
        public void TestIfPathForDeleteFileIsEmptyReturnExcetion()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandDelete}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandDelete);

            //Assert
            Assert.That(() => { m_FileWorker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.CommandMustBeWithParametrs));
        }


        /// <summary>
        ///     Checks that the file has been deleted.  
        /// </summary>
        [Test]
        public void TestThatSpecifiedFileHasBeenDeleted()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandDelete} {m_profile1Mock.Object.ProfileId}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandDelete);

            //Assert
            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл для тестирования удаления не создан!");
            
            m_FileWorker.Execute();

            Assert.IsFalse(File.Exists(pathToProfile1Mock));
        }

        /// <summary>
        ///     Checks that the exception will be thrown when try to find file by wrong path.  
        /// </summary>
        [Test]
        public void TestWrongPathForFindFileReturnExcetion()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandFind} asdf";

            //Act
            GetFileWorker(commandText, CommandsList.CommandFind);

            //Assert
            Assert.That(() => { m_FileWorker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.FileNotExists));
        }

        /// <summary>
        ///     Checks that the exception will be thrown when try to find the file by empty path.  
        /// </summary>
        [Test]
        public void TestIfPathForFindFileIsEmptyReturnExcetion()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandFind}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandFind);

            //Assert
            Assert.That(() => { m_FileWorker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.CommandMustBeWithParametrs));
        }


        /// <summary>
        ///     Checks that the file has been shown.  
        /// </summary>
        [Test]
        public void TestThatSpecifiedFileHasBeenShown()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandFind} {m_profile1Mock.Object.ProfileId}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandFind);
            m_ExpectedMessage.Add("1. ФИО: Рэй Бредбери");
            //Assert
            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл для тестирования поиска не создан!");

            m_FileWorker.Execute();

           CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks that all saved profiles are shown.  
        /// </summary>
        [Test]
        public void TestCommandListReturnListOfThreeFiles()
        {
            //Arrange
            string commandText = CommandsList.CommandList; 

            //Act
            GetFileWorker(commandText, CommandsList.CommandList);
            m_ExpectedMessage.Add("123-123-123456-1");
            m_ExpectedMessage.Add("123-123-123456-2");
            m_ExpectedMessage.Add("123-123-123456-3");

            //Assert
            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile2Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile3Mock), "Файл для тестирования поиска не создан!");
            File.SetCreationTime(pathToProfile1Mock, new DateTime(2017, 08, 28));

            m_FileWorker.Execute();

            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks that all saved profiles are shown.  
        /// </summary>
        [Test]
        public void TestCommandListWithAdditionalTextReturnException()
        {
            //Arrange
            string commandText =$"{CommandsList.CommandList} {m_profile1Mock.Object.ProfileId}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandList);
            m_ExpectedMessage.Add("123-123-123456-1");
            m_ExpectedMessage.Add("123-123-123456-2");
            m_ExpectedMessage.Add("123-123-123456-3");

            //Assert
            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile2Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile3Mock), "Файл для тестирования поиска не создан!");
            File.SetCreationTime(pathToProfile1Mock, new DateTime(2017, 08, 28));

            Assert.That(() => { m_FileWorker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.CommandNotCorrect));
        }

        /// <summary>
        ///    Checks that only today saved profiles are shown.  
        /// </summary>
        [Test]
        public void TestCommandListReturnListTodayOfTwoFiles()
        {
            //Arrange
            string commandText = CommandsList.CommandListToday; //$"{CommandsList.CommandList} {m_profile1Mock.Object.ProfileId}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandListToday);
           
            m_ExpectedMessage.Add("123-123-123456-2");
            m_ExpectedMessage.Add("123-123-123456-3");


            //Assert
            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile2Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile3Mock), "Файл для тестирования поиска не создан!");

            File.SetCreationTime(pathToProfile1Mock, new DateTime(2017, 08, 28));

            m_FileWorker.Execute();

            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }


        /// <summary>
        ///     Checks that all saved profiles are shown.  
        /// </summary>
        [Test]
        public void TestCommandListTodayWithAdditionalTextReturnException()
        {
            //Arrange
            string commandText = $"{CommandsList.CommandListToday} {m_profile1Mock.Object.ProfileId}";

            //Act
            GetFileWorker(commandText, CommandsList.CommandListToday);
            m_ExpectedMessage.Add("123-123-123456-1");
            m_ExpectedMessage.Add("123-123-123456-2");
            m_ExpectedMessage.Add("123-123-123456-3");

            //Assert
            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile2Mock), "Файл для тестирования поиска не создан!");
            Assert.IsTrue(File.Exists(pathToProfile3Mock), "Файл для тестирования поиска не создан!");
            File.SetCreationTime(pathToProfile1Mock, new DateTime(2017, 08, 28));

            Assert.That(() => { m_FileWorker.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.CommandNotCorrect));
        }

        private void GetFileWorker(string input, string command)
        {
            var fwICommand = m_container.Resolve<ICommand>(command
                , new ParameterOverride("command", input)
                , new ParameterOverride("name", command)
                , new PropertyOverride("WriterAndReaderWorker", m_Writer.Object));

            m_FileWorker = fwICommand as FileWorker;
            Assert.AreNotEqual(null, m_FileWorker, "Ошибка при DI. Проверить настройки UNITY");
        }

       
        private IUnityContainer m_container;
        Mock<IProfile> m_profile1Mock = new Mock<IProfile>();
        Mock<IProfile> m_profile2Mock = new Mock<IProfile>();
        Mock<IProfile> m_profile3Mock = new Mock<IProfile>();
        Mock<IWriterAndReader> m_Writer = new Mock<IWriterAndReader>();
        private FileWorker m_FileWorker;

        private string pathToProfile1Mock;
        private string pathToProfile2Mock;
        private string pathToProfile3Mock;

        private bool m_IfDirCreate;
        private List<string> m_OutputMessages;
        private List<string> m_ExpectedMessage;
    }
}
