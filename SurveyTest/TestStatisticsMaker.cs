using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    ///     Tests public  methods of the StatisticsMaker class.
    /// </summary>
    [TestFixture]
    public class TestStatisticsMaker
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
                if (File.Exists(m_pathToProfile1Mock))
                    File.Delete(m_pathToProfile1Mock);

                if (File.Exists(m_pathToProfile2Mock))
                    File.Delete(m_pathToProfile2Mock);

                if (File.Exists(m_pathToProfile3Mock))
                    File.Delete(m_pathToProfile3Mock);

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

            List<ProfileItem> list1 = new List<ProfileItem>{m_ProfileItemFIO1,m_ProfileItemBd1,m_ProfileItemLn1,m_ProfileIteExp1};
            List<ProfileItem> list2 = new List<ProfileItem> { m_ProfileItemFIO2, m_ProfileItemBd2, m_ProfileItemLn2, m_ProfileIteExp2 };
            List<ProfileItem> list3 = new List<ProfileItem> { m_ProfileItemFIO3, m_ProfileItemBd3, m_ProfileItemLn3, m_ProfileIteExp3 };

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

            List<ProfileItem> list1 = new List<ProfileItem> {  m_ProfileItemBd1, m_ProfileItemLn1, m_ProfileIteExp1 };
            List<ProfileItem> list2 = new List<ProfileItem> {  m_ProfileItemBd2, m_ProfileItemLn2, m_ProfileIteExp2 };
            List<ProfileItem> list3 = new List<ProfileItem> {  m_ProfileItemBd3, m_ProfileItemLn3, m_ProfileIteExp3 };

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

            List<ProfileItem> list1 = new List<ProfileItem> { m_ProfileItemFIO1, m_ProfileItemBd1,  m_ProfileIteExp1 };
            List<ProfileItem> list2 = new List<ProfileItem> { m_ProfileItemFIO2, m_ProfileItemBd2,  m_ProfileIteExp2 };
            List<ProfileItem> list3 = new List<ProfileItem> { m_ProfileItemFIO3, m_ProfileItemBd3,  m_ProfileIteExp3 };

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

            m_pathToProfile1Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile1Mock.Object.ProfileId}.txt");

            m_pathToProfile2Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile2Mock.Object.ProfileId}.txt");

            m_pathToProfile3Mock = Path.Combine(SurveyConst.DirectoryName,
                $"{m_profile3Mock.Object.ProfileId}.txt");

            var saver1 = m_container.Resolve<ICommand>(CommandsList.CommandSave
                , new PropertyOverride("WorkingProfile", m_profile1Mock.Object));

            Saver saver = saver1 as Saver;

            saver.Execute();
            saver.WorkingProfile = m_profile2Mock.Object;
            saver.Execute();
            saver.WorkingProfile = m_profile3Mock.Object;
            saver.Execute();

            Assert.IsTrue(File.Exists(m_pathToProfile1Mock), "Файл 1 для тестирования статистики не создан!");
            Assert.IsTrue(File.Exists(m_pathToProfile2Mock), "Файл 2 для тестирования статистики не создан!");
            Assert.IsTrue(File.Exists(m_pathToProfile3Mock), "Файл 3 для тестирования статистики не создан!");
        }

        private IUnityContainer m_container;
        Mock<IProfile> m_profile1Mock = new Mock<IProfile>();
        Mock<IProfile> m_profile2Mock = new Mock<IProfile>();
        Mock<IProfile> m_profile3Mock = new Mock<IProfile>();
        Mock<IWriterAndReader> m_Writer = new Mock<IWriterAndReader>();
      
        private ICommand m_StatisticMaker;

        private string m_pathToProfile1Mock;
        private string m_pathToProfile2Mock;
        private string m_pathToProfile3Mock;
        
        private bool m_IfDirCreate;
        private List<string> m_OutputMessages;
        private List<string> m_ExpectedMessage;

        private readonly ProfileItem m_ProfileItemFIO1 = new ProfileItem(new QuestionString("ФИО", "Введите ФИО", 0, 300), "Рэй Бредбери");
        private readonly ProfileItem m_ProfileItemFIO2 = new ProfileItem(new QuestionString("ФИО", "Введите ФИО", 0, 300), "Азйек Азимов");
        private readonly ProfileItem m_ProfileItemFIO3 = new ProfileItem(new QuestionString("ФИО", "Введите ФИО", 0, 300), "Льюис МакМастер Буджол");

        private readonly ProfileItem m_ProfileItemBd1 = new ProfileItem(new QuestionDate("Дата рождения","Введите дату рождения в формате ДД.ММ.ГГГГ"
            , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1975");
        private readonly ProfileItem m_ProfileItemBd2 = new ProfileItem(new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
            , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1985");
        private readonly ProfileItem m_ProfileItemBd3 = new ProfileItem(new QuestionDate("Дата рождения", "Введите дату рождения в формате ДД.ММ.ГГГГ"
            , DateTime.Now.AddYears(-SurveyConst.MaximumLifeSpan), DateTime.Now), "10.10.1995");

        private readonly ProfileItem m_ProfileItemLn1 = new ProfileItem(new QuestionValuesList("Любимый язык программирования"
            , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
            , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "С");

        private readonly ProfileItem m_ProfileItemLn2 = new ProfileItem(new QuestionValuesList("Любимый язык программирования"
            , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
            , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "Java");

        private readonly ProfileItem m_ProfileItemLn3 = new ProfileItem(new QuestionValuesList("Любимый язык программирования"
            , "Укажите любимый язык программирования (PHP, JavaScript, C, C++, Java, C#, Python, Ruby)"
            , new List<string> { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" }), "Java");

        private readonly ProfileItem m_ProfileIteExp1 = new ProfileItem(new QuestionInt("Опыт программирования на указанном языке"
            , "Введите опыт программирования на указанном языке (Полных лет)", SurveyConst.MaximumLifeSpan,0), "10");

        private readonly ProfileItem m_ProfileIteExp2 = new ProfileItem(new QuestionInt("Опыт программирования на указанном языке"
            , "Введите опыт программирования на указанном языке (Полных лет)", SurveyConst.MaximumLifeSpan, 0), "8");

        private readonly ProfileItem m_ProfileIteExp3 = new ProfileItem(new QuestionInt("Опыт программирования на указанном языке"
            , "Введите опыт программирования на указанном языке (Полных лет)", SurveyConst.MaximumLifeSpan, 0), "15");
    }
}
