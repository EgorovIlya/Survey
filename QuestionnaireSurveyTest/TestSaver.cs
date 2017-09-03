using System;
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
    ///     Tests public  methods of the TestSaver class.
    /// </summary>
    [TestFixture]
    public class TestSaver
    {
        [OneTimeSetUp]
        public void Init()
        {
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);
            m_IfDirCreate = Directory.Exists(SurveyConst.DirectoryName);

            m_profileEmptyMock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300)),
               
            });
            m_profileEmptyMock.Setup(a => a.ProfileId).Returns("123-123-123456-1");
           

            m_profilePartiallyCompletedMock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300) , "Айзек Азимов"),

                new ProfileItem( new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
                    , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan)  ,DateTime.Now)),
               
            });
            m_profilePartiallyCompletedMock.Setup(a => a.ProfileId).Returns("123-123-123456-2");

            m_profileFullyCompletedMock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300) , "Айзек Азимов"),

                new ProfileItem( new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
                    , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan)  ,DateTime.Now),"22.11.1989"),

            });
            m_profileFullyCompletedMock.Setup(a => a.ProfileId).Returns("123-123-123456-4");


            pathToProfileEmptyMock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profileEmptyMock.Object.ProfileId}.txt");

            pathToProfilePartiallyCompletedMock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profilePartiallyCompletedMock.Object.ProfileId}.txt");

            pathToProfileFullyCompletedMock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profileFullyCompletedMock.Object.ProfileId}.txt");

        }

        [TearDown]
        public void CleanUp()
        {
            if(!m_IfDirCreate && Directory.Exists(SurveyConst.DirectoryName))
                Directory.Delete(SurveyConst.DirectoryName,true);
            else
            {
                if (File.Exists(pathToProfileEmptyMock))
                    File.Delete(pathToProfileEmptyMock);

                if (File.Exists(pathToProfilePartiallyCompletedMock))
                    File.Delete(pathToProfilePartiallyCompletedMock);

                if (File.Exists(pathToProfileFullyCompletedMock))
                    File.Delete(pathToProfileFullyCompletedMock);
                Console.WriteLine("All Clear!");
            }
        }


        /// <summary>
        ///     Checks that the exception was thrown when trying to save an empty profile.  
        /// </summary>
        [Test]
        public void TestTryingSaveEmptyProfileReturnException()
        {
            //Arrange
            var saver = m_container.Resolve<ICommand>(CommandsList.CommandSave
                , new PropertyOverride("WorkingProfile", m_profileEmptyMock.Object));

            //Assert
            Assert.That(() => { saver.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.ProfileIsNotCorrect));

        }

        /// <summary>
        ///     Checks that the exception was thrown when trying to save an partially completed profile.  
        /// </summary>
        [Test]
        public void TestTryingSavePartiallyCompletedReturnException()
        {
            //Arrange
            var saver = m_container.Resolve<ICommand>(CommandsList.CommandSave
                , new PropertyOverride("WorkingProfile", m_profilePartiallyCompletedMock.Object));

            //Assert
            Assert.That(() => { saver.Execute(); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.ProfileIsNotCorrect));

        }

        /// <summary>
        ///     Checks that the exception was thrown when trying to save an partially completed profile.  
        /// </summary>
        [Test]
        public void TestTryingSaveFullyCompletedReturnFileInResultDirectory()
        {
            //Arrange
            var saver = m_container.Resolve<ICommand>(CommandsList.CommandSave
                , new PropertyOverride("WorkingProfile", m_profileFullyCompletedMock.Object));

            bool expected = true;
            List<string> expectedTxt = new List<string> { "1. ФИО: Айзек Азимов", "2. Дата рождения: 22.11.1989" };
            List<string> actualTxt = new List<string>();

            //Act
            saver.Execute();

            bool actual = File.Exists(pathToProfileFullyCompletedMock);
            if (actual)
            {
                using (StreamReader r = new StreamReader(pathToProfileFullyCompletedMock, true))
                {
                    string srline;
                    while ((srline = r.ReadLine()) != null)
                    {
                        actualTxt.Add(srline);
                    }
                }
            }

            //Assert
            Assert.AreEqual(expected, actual, "File was not created!!!");
            CollectionAssert.AreEqual(expectedTxt, actualTxt);

        }

        private IUnityContainer m_container;
        Mock<IProfile> m_profileEmptyMock = new Mock<IProfile>();
        Mock<IProfile>  m_profilePartiallyCompletedMock = new Mock<IProfile>();
        Mock<IProfile> m_profileFullyCompletedMock = new Mock<IProfile>();

        private string pathToProfileEmptyMock;
        private string pathToProfilePartiallyCompletedMock;
        private string pathToProfileFullyCompletedMock;
      
        private bool m_IfDirCreate;
        private List<string> m_OutputMessages;
        private List<string> m_ExpectedMessage;
    }
}
