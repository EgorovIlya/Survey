using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Survey;
using Survey.Controllers.Commands;
using Survey.Interface;
using Survey.ModelsDTO;
using Survey.ModelsDTO.ProfileQuestions;
using Survey.Utils;


namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the ProfileController class.
    /// </summary>
    [TestFixture]
    public class TestProfileController
    {
        /// <summary>
        ///     Initializes common data for all tests.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);
            m_Writer.Setup(c => c.WriteLine(It.IsAny<string>())).Callback((string s) => m_OutputMessages.Add(s));
        }

        /// <summary>
        ///     Initializes beginning data for each test.
        /// </summary>
        [SetUp]
        public void InitPerTest()
        {
            m_profileEmptyMock.Setup(a => a.Items).Returns(new List<ProfileItem>()
            {
                new ProfileItem( new QuestionString("ФИО", "Введите ФИО",0, 300)),

                new ProfileItem( new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
                    , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan)  ,DateTime.Now)),

                new ProfileItem( new QuestionValuesList("Любимый язык программирования"
                    , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
                    , new List<string> {"PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" } )),

                new ProfileItem( new QuestionInt("Опыт программирования на указанном языке"
                    , "Введите опыт программирования на указанном языке (Полных лет)",0,SurveyConst.MaximumLifeSpan)),

                new ProfileItem( new QuestionPhoneNumber("Мобильный телефон", "Укажите номер мобильного телефона")),
            });

            m_profileEmptyMock.Setup(a => a.ProfileId).Returns("123-3333-1231");

            m_OutputMessages = new List<string>();

            var pcICommand = m_container.Resolve<ICommand>(CommandsList.CommandNewProfile
                , new PropertyOverride("WorkingProfile", m_profileEmptyMock.Object)
                , new PropertyOverride("WriterAndReaderWorker", m_Writer.Object));

            m_ProfileConroller = pcICommand as ProfileConroller;

            //Assert
            Assert.AreNotEqual(null, m_ProfileConroller, "Ошибка при DI. Проверить настройки UNITY");

            m_ExpectedMessage = DefaultExpectedMessage();

        }

        /// <summary>
        ///    Creates a list of messages that should be written in the console.
        /// </summary>
        /// <returns></returns>
        private List<string> DefaultExpectedMessage()
        {
            List<string>  expectedMessages = new List<string>();
            expectedMessages.Add(m_profileEmptyMock.Object.Items[0].Question.QuestionText);
            expectedMessages.Add(m_profileEmptyMock.Object.Items[1].Question.QuestionText);
            expectedMessages.Add(m_profileEmptyMock.Object.Items[2].Question.QuestionText);
            expectedMessages.Add(m_profileEmptyMock.Object.Items[3].Question.QuestionText);
            expectedMessages.Add(m_profileEmptyMock.Object.Items[4].Question.QuestionText);
            expectedMessages.Add($"{SurveyConst.SaveProfile}{CommandsList.CommandSave}");
            expectedMessages.Add($"{SurveyConst.StartMessages}");
            return expectedMessages;
        }

        /// <summary>
        ///     Checks output messages, if all answers is correct.
        /// </summary>
        [Test]
        public void TestAllCorrectAnswerForAllQuestion()
        {
            //Arrange
            m_Writer.SetupSequence(c => c.ReadLine())
                                    .Returns(name)
                                    .Returns(birthDay)
                                    .Returns(lang)
                                    .Returns(expirience)
                                    .Returns(mobilePhone);
            //Act
            m_ProfileConroller.Execute();
            
            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks output messages, if first question was specified and inputted command "go to previous".
        /// </summary>
        [Test]
        public void TestTryGoToPrevioseWhenFirstQuestionReturnErrorText()
        {
            //Arrange
            m_Writer.SetupSequence(c => c.ReadLine())
                .Returns(CommandsList.CommandProfileGoToPrevioseQuestion)
                .Returns(name)
                .Returns(birthDay)
                .Returns(lang)
                .Returns(expirience)
                .Returns(mobilePhone);

            m_ExpectedMessage.Insert(0, m_profileEmptyMock.Object.Items[0].Question.QuestionText);
            m_ExpectedMessage.Insert(1, ErrorMessages.FirstElementWasReached);

            //Act
            m_ProfileConroller.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }


        /// <summary>
        ///     Checks output messages, if fourth question was specified and inputted command "go to first".
        /// </summary>
        [Test]
        public void TestTryGoTo1From4Return1QuestionTextAnd4FourthQuestion()
        {
            //Arrange
            m_Writer.SetupSequence(c => c.ReadLine())
                .Returns(name)
                .Returns(birthDay)
                .Returns(lang)
                .Returns($"{CommandsList.CommandProfileGoToQuestion} 1")
                .Returns(name)
                .Returns(expirience)
                .Returns(mobilePhone);

            m_ExpectedMessage.Insert(4, m_profileEmptyMock.Object.Items[0].Question.QuestionText);
            m_ExpectedMessage.Insert(5, m_profileEmptyMock.Object.Items[3].Question.QuestionText);

            //Act
            m_ProfileConroller.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks output messages, if fourth question was specified and inputted command "go to previous".
        /// </summary>
        [Test]
        public void TestTryGoToPrevioseFrom4Return3QuestionText()
        {
            //Arrange
            m_Writer.SetupSequence(c => c.ReadLine())
                .Returns(name)
                .Returns(birthDay)
                .Returns(lang)
                .Returns($"{CommandsList.CommandProfileGoToQuestion} 1")
                .Returns(name)
                .Returns(expirience)
                .Returns(mobilePhone);

            m_ExpectedMessage.Insert(4, m_profileEmptyMock.Object.Items[0].Question.QuestionText);
            m_ExpectedMessage.Insert(5, m_profileEmptyMock.Object.Items[3].Question.QuestionText);

            //Act
            m_ProfileConroller.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks output messages, if fourth question was specified and inputted command "restart".
        /// </summary>
        [Test]
        public void TestCommandClearWhen4QuestionReturnProfileRestart()
        {
            //Arrange
            m_Writer.SetupSequence(c => c.ReadLine())
                .Returns(name)
                .Returns(birthDay)
                .Returns(lang)
                .Returns(CommandsList.CommandProfileRestart)
                .Returns(name)
                .Returns(birthDay)
                .Returns(lang)
                .Returns(expirience)
                .Returns(mobilePhone);

            m_ExpectedMessage.Insert(4, m_profileEmptyMock.Object.Items[0].Question.QuestionText);
            m_ExpectedMessage.Insert(5, m_profileEmptyMock.Object.Items[1].Question.QuestionText);
            m_ExpectedMessage.Insert(6, m_profileEmptyMock.Object.Items[2].Question.QuestionText);
            m_ExpectedMessage.Insert(7, m_profileEmptyMock.Object.Items[3].Question.QuestionText);

            //Act
            m_ProfileConroller.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }


        /// <summary>
        ///     Checks output messages, if fourth question was specified and inputted answer was not corrected.
        /// </summary>
        [Test]
        public void TestInputIncorrectAnswerOn4QuestionReturnErrorAnd4QuestionAgain()
        {
            //Arrange
            m_Writer.SetupSequence(c => c.ReadLine())
                .Returns(name)
                .Returns(birthDay)
                .Returns(lang)
                .Returns("пять")
                .Returns(expirience)
                .Returns(mobilePhone);

            m_ExpectedMessage.Insert(4, ErrorMessages.IntFormatError);
            m_ExpectedMessage.Insert(5, m_profileEmptyMock.Object.Items[3].Question.QuestionText);

            //Act
            m_ProfileConroller.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        private IUnityContainer m_container;
        Mock<IProfile> m_profileEmptyMock = new Mock<IProfile>();
        Mock<IWriterAndReader> m_Writer = new Mock<IWriterAndReader>();
        private ProfileConroller m_ProfileConroller;

        private const string name = "АрмиН Норд";
        private const string birthDay = "22.11.1981";
        private const string lang = "C#";
        private const string expirience = "8";
        private const string mobilePhone = "+79114971497";

        private List<string> m_OutputMessages;
        private List<string> m_ExpectedMessage;
    }
}
