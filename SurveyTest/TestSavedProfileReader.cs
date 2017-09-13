using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Survey;
using Survey.Controllers.Commands;
using Survey.Interface;
using Survey.ModelsDTO;
using Survey.ModelsDTO.ProfileQuestions;
using Survey.Utils;

namespace SurveyTest
{


    /// <summary>
    /// Summary description for TestSavedProfileReader
    /// </summary>
    [TestFixture]
    public class TestSavedProfileReader
    {
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
                new ProfileItem(new QuestionDate("Дата рождения","Введите дату рождения в формате ДД.ММ.ГГГГ"
                    , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1975"),
                new ProfileItem(new QuestionValuesList("Любимый язык программирования"
                    , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
                    , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "C")
        });

            m_profile1Mock.Setup(a => a.ProfileId).Returns("123-123-123456-1");
            
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
                Console.WriteLine("All Clear!");
            }
        }

        [Test]
        public void TestReturnProfileStrings()
        {
            //Arrange
            m_ExpectedMessage.Add("1. ФИО: Рэй Бредбери");
            m_ExpectedMessage.Add("2. Дата рождения: 10.10.1975");
            m_ExpectedMessage.Add("3. Любимый язык программирования: C");
            m_ExpectedMessage.Add($"Анкета заполнена: {DateTime.Now.ToString(SurveyConst.FormatDate)}");

            //Act
            SavedProfileReader spr = new SavedProfileReader(pathToProfile1Mock);
            
            //Assert
            
            CollectionAssert.AreEqual(m_ExpectedMessage, spr.ProfileStrings,
                "Строки сохраненной анкеты не соответствуют образцу!");
        }


        [Test]
        public void WrongPathToProfile()
        {
            //Arrange
          
            //Act
           
            //Assert

            Assert.That(() =>
            {
                SavedProfileReader spr = new SavedProfileReader("C\\WrongDir");
            }, Throws.TypeOf<DirectoryNotFoundException>());
        }


        [Test]
        public void EmptyProfile()
        {
            //Arrange
           
            //Act
            SavedProfileReader spr = new SavedProfileReader( new StringReader(""));
           
            //Assert
            Assert.IsFalse(spr.ProfileStrings.Any());
           
        }

        [Test]
        public void TestGetAnswerFromSavedProfile()
        {
            //Arrange
            string expected = "Рэй Бредбери";

            //Act
            SavedProfileReader spr = new SavedProfileReader(pathToProfile1Mock);
            string actual = spr.GetAnswerFromSavedProfile(spr.ProfileStrings[0], SurveyConst.Separator);
            //Assert
            Assert.AreEqual(expected, actual, "Возвращенная строка отличается от образца!");
        }


        [Test]
        public void TestGetAnswerIfStringWithoutSeparator()
        {
            //Arrange
            string expected = "";

            //Act
            SavedProfileReader spr = new SavedProfileReader(pathToProfile1Mock);
            string actual = spr.GetAnswerFromSavedProfile("12345679",SurveyConst.Separator);
            //Assert
            Assert.AreEqual(expected, actual, "Возвращенная строка отличается от образца!");
        }


        private IUnityContainer m_container;
        Mock<IProfile> m_profile1Mock = new Mock<IProfile>();
        Mock<IWriterAndReader> m_Writer = new Mock<IWriterAndReader>();
        private FileWorker m_FileWorker;

        private string pathToProfile1Mock;

        private bool m_IfDirCreate;

        private List<string> m_OutputMessages;
        private List<string> m_ExpectedMessage;
    }
}
