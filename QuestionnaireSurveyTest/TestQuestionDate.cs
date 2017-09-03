using System;
using NUnit.Framework;
using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestQuestionString class.
    /// </summary>
    [TestFixture]
    public class TestQuestionDate
    {
        /// <summary>
        ///     Initializes beginning data before all tests are run.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            
        }

        /// <summary>
        ///     Initializes beginning data before each test is run.
        /// </summary>
        [SetUp]
        public void Start()
        {
            m_QuestionDate = new QuestionDate("Дата", "Дата в формате ДД.ММ.ГГГГ"
                , new DateTime(1930, 01, 01)
                , new DateTime(2017, 08, 01));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer is not ДД.ММ.ГГГГ.  
        /// </summary>
        [Test]
        public void TestAnswerNotIntReturnException()
        {
            //Arrange
            string answer = "пять";

            //Act
           
            //Assert
            Assert.That(() => { m_QuestionDate.CheckedAnswer(answer);  }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.DateFormatError));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer int is too big.  
        /// </summary>
        [Test]
        public void TestAnswerWithTooManyCharsReturnException()
        {
            //Arrange
            string answer = new DateTime(2018,12,01).ToString(SurveyConst.FormatDate);

            //Act

            //Assert
            Assert.That(() => { m_QuestionDate.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.DateMustBeLess));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer length is too small.  
        /// </summary>
        [Test]
        public void TestIfPararmsNotCorretReturnException()
        {
            //Arrange
            string answer = new DateTime(1900, 12, 01).ToString(SurveyConst.FormatDate); 

            //Act

            //Assert
            Assert.That(() => { m_QuestionDate.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.DateMustBeGreater));
        }

        /// <summary>
        ///     Checks if  answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer()
        {
            //Arrange
            string answerExpected = new DateTime(1975, 01, 01).ToString(SurveyConst.FormatDate); ;

            //Act
            string answerActual = m_QuestionDate.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual);
        }

        private QuestionDate m_QuestionDate;
    }
}
