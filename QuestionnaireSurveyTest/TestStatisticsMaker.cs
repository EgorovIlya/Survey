using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
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
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the StatisticsMaker class.
    /// </summary>
    [TestFixture]
    public class TestStatisticsMaker
    {
        [OneTimeSetUp]
        public void Init()
        {
            m_OutputMessages = new List<string>();
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);
            m_Writer.Setup(c => c.WriteLine(It.IsAny<string>())).Callback((string s) => m_OutputMessages.Add(s));

            m_IfDirCreate = Directory.Exists(SurveyConst.DirectoryName);

      
        }

        /// <summary>
        ///     Initializes beginning data before each test is run.
        /// </summary>
        [SetUp]
        public void Start()
        {
            m_ExpectedMessage = new List<string>();
            m_OutputMessages.Clear();

            m_StatisticMaker = m_container.Resolve<ICommand>(CommandsList.CommandStatistic
                , new PropertyOverride("WriterAndReaderWorker", m_Writer.Object));
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
        ///     Checks statistics if all profiles are correct.  
        /// </summary>
        [Test]
        public void TestStatisticsMakerWithCorrectDataReturnExpectedData()
        {
            //Arrange
            m_ExpectedMessage.Add("Средний возраст всех опрошенных: 31 год");
            m_ExpectedMessage.Add("Самый популярный язык программирования: Java");
            m_ExpectedMessage.Add("Самый опытный программист: Льюис МакМастер Буджол");

            List<ProfileItem> list1 = new List<ProfileItem>{ProfileItemFIO1,ProfileItemBd1,ProfileItemLn1,ProfileIteExp1};
            List<ProfileItem> list2 = new List<ProfileItem> { ProfileItemFIO2, ProfileItemBd2, ProfileItemLn2, ProfileIteExp2 };
            List<ProfileItem> list3 = new List<ProfileItem> { ProfileItemFIO3, ProfileItemBd3, ProfileItemLn3, ProfileIteExp3 };

            //Act
            CreateAndSaveProfiles(list1, list2, list3);
            m_StatisticMaker.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks statistics if all profiles without FIO.  
        /// </summary>
        [Test]
        public void TestWhenProfileWithOutFIO()
        {
            //Arrange
            m_ExpectedMessage.Add("Средний возраст всех опрошенных: 31 год");
            m_ExpectedMessage.Add("Самый популярный язык программирования: Java");
            m_ExpectedMessage.Add("Самый опытный программист: Нет данных!");

            List<ProfileItem> list1 = new List<ProfileItem> {  ProfileItemBd1, ProfileItemLn1, ProfileIteExp1 };
            List<ProfileItem> list2 = new List<ProfileItem> {  ProfileItemBd2, ProfileItemLn2, ProfileIteExp2 };
            List<ProfileItem> list3 = new List<ProfileItem> {  ProfileItemBd3, ProfileItemLn3, ProfileIteExp3 };

            //Act
            CreateAndSaveProfiles(list1, list2, list3);
            m_StatisticMaker.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks statistics if all profiles without programming language.  
        /// </summary>
        [Test]
        public void TestWhenProfileWithOutProgrammingLamguages()
        {
            //Arrange
            m_ExpectedMessage.Add("Средний возраст всех опрошенных: 31 год");
            m_ExpectedMessage.Add("Самый популярный язык программирования: Нет данных!");
            m_ExpectedMessage.Add("Самый опытный программист: Льюис МакМастер Буджол");

            List<ProfileItem> list1 = new List<ProfileItem> { ProfileItemFIO1, ProfileItemBd1,  ProfileIteExp1 };
            List<ProfileItem> list2 = new List<ProfileItem> { ProfileItemFIO2, ProfileItemBd2,  ProfileIteExp2 };
            List<ProfileItem> list3 = new List<ProfileItem> { ProfileItemFIO3, ProfileItemBd3,  ProfileIteExp3 };

            //Act
            CreateAndSaveProfiles(list1, list2, list3);
            m_StatisticMaker.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks statistics if all profiles are empty.  
        /// </summary>
        [Test]
        public void TestWhenProfilesAreEmpty()
        {
            //Arrange
            m_ExpectedMessage.Add("Средний возраст всех опрошенных: Нет данных!");
            m_ExpectedMessage.Add("Самый популярный язык программирования: Нет данных!");
            m_ExpectedMessage.Add("Самый опытный программист: Нет данных!");

            List<ProfileItem> list1 = new List<ProfileItem> {  };
            List<ProfileItem> list2 = new List<ProfileItem> {  };
            List<ProfileItem> list3 = new List<ProfileItem> {  };

            //Act
            CreateAndSaveProfiles(list1, list2, list3);
            m_StatisticMaker.Execute();

            //Assert
            CollectionAssert.AreEqual(m_ExpectedMessage, m_OutputMessages);
        }

        /// <summary>
        ///     Checks statistics if all profiles are correct.  
        /// </summary>
        [Test]
        public void TestDate()
        {
            string birthday = "10.10.1975";

            DateTime dateBirthday;

            if (!DateTime.TryParseExact(birthday, SurveyConst.FormatDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateBirthday))
            Console.WriteLine(dateBirthday);

        }


        private void CreateAndSaveProfiles(List<ProfileItem> profile1, List<ProfileItem> profile2, List<ProfileItem> profile3)
        {
            m_profile1Mock.Setup(a => a.Items).Returns(profile1);
            m_profile1Mock.Setup(a => a.ProfileId).Returns("123-123-123456-1");

            m_profile2Mock.Setup(a => a.Items).Returns(profile2);
            m_profile2Mock.Setup(a => a.ProfileId).Returns("123-123-123456-2");

            m_profile3Mock.Setup(a => a.Items).Returns(profile3);
            m_profile3Mock.Setup(a => a.ProfileId).Returns("123-123-123456-3");

            pathToProfile1Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile1Mock.Object.ProfileId}.txt");

            pathToProfile2Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile2Mock.Object.ProfileId}.txt");

            pathToProfile3Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile3Mock.Object.ProfileId}.txt");

            var saver1 = m_container.Resolve<ICommand>(CommandsList.CommandSave
                , new PropertyOverride("WorkingProfile", m_profile1Mock.Object));

            Saver saver = saver1 as Saver;

            saver.Execute();
            saver.WorkingProfile = m_profile2Mock.Object;
            saver.Execute();
            saver.WorkingProfile = m_profile3Mock.Object;
            saver.Execute();

            Assert.IsTrue(File.Exists(pathToProfile1Mock), "Файл 1 для тестирования статистики не создан!");
            Assert.IsTrue(File.Exists(pathToProfile2Mock), "Файл 2 для тестирования статистики не создан!");
            Assert.IsTrue(File.Exists(pathToProfile3Mock), "Файл 3 для тестирования статистики не создан!");
        }

        private IUnityContainer m_container;
        Mock<IProfile> m_profile1Mock = new Mock<IProfile>();
        Mock<IProfile> m_profile2Mock = new Mock<IProfile>();
        Mock<IProfile> m_profile3Mock = new Mock<IProfile>();
        Mock<IWriterAndReader> m_Writer = new Mock<IWriterAndReader>();
      
        private ICommand m_StatisticMaker;

        private string pathToProfile1Mock;
        private string pathToProfile2Mock;
        private string pathToProfile3Mock;

        private bool m_IfDirCreate;
        private List<string> m_OutputMessages;
        private List<string> m_ExpectedMessage;

        private ProfileItem ProfileItemFIO1 = new ProfileItem(new QuestionString("ФИО", "Введите ФИО", 0, 300), "Рэй Бредбери");
        private ProfileItem ProfileItemFIO2 = new ProfileItem(new QuestionString("ФИО", "Введите ФИО", 0, 300), "Азйек Азимов");
        private ProfileItem ProfileItemFIO3 = new ProfileItem(new QuestionString("ФИО", "Введите ФИО", 0, 300), "Льюис МакМастер Буджол");

        private ProfileItem ProfileItemBd1 = new ProfileItem(new QuestionDate("Дата рождения","Введите дату рождения в формате ДД.ММ.ГГГГ"
            , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1975");
        private ProfileItem ProfileItemBd2 = new ProfileItem(new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
            , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1985");
        private ProfileItem ProfileItemBd3 = new ProfileItem(new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
            , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1995");

        private ProfileItem ProfileItemLn1 = new ProfileItem(new QuestionValuesList("Любимый язык программирования"
            , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
            , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "С");

        private ProfileItem ProfileItemLn2 = new ProfileItem(new QuestionValuesList("Любимый язык программирования"
            , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
            , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "Java");

        private ProfileItem ProfileItemLn3 = new ProfileItem(new QuestionValuesList("Любимый язык программирования"
            , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
            , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "Java");

        private ProfileItem ProfileIteExp1 = new ProfileItem(new QuestionInt("Опыт программирования на указанном языке"
            , "Введите опыт программирования на указанном языке (Полных лет)", SurveyConst.MaximumLifeSpan,0), "10");

        private ProfileItem ProfileIteExp2 = new ProfileItem(new QuestionInt("Опыт программирования на указанном языке"
            , "Введите опыт программирования на указанном языке (Полных лет)", SurveyConst.MaximumLifeSpan, 0), "8");

        private ProfileItem ProfileIteExp3 = new ProfileItem(new QuestionInt("Опыт программирования на указанном языке"
            , "Введите опыт программирования на указанном языке (Полных лет)", SurveyConst.MaximumLifeSpan, 0), "15");
    }
}
