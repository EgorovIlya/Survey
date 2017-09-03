using NUnit.Framework;
using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestQuestionString class.
    /// </summary>
    [TestFixture]
    public class TestQuestionInt
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
            m_QuestionInt = new QuestionInt("Лет", "Введите возраст", 10, 100);
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer is not int.  
        /// </summary>
        [Test]
        public void TestAnswerNotIntReturnException()
        {
            //Arrange
            string answer = "пять";

            //Act
           
            //Assert
            Assert.That(() => { m_QuestionInt.CheckedAnswer(answer);  }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.IntFormatError));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer int is too big.  
        /// </summary>
        [Test]
        public void TestAnswerWithTooManyCharsReturnException()
        {
            //Arrange
            string answer = "3000";

            //Act

            //Assert
            Assert.That(() => { m_QuestionInt.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.NumberMustBeLessMax));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer length is too small.  
        /// </summary>
        [Test]
        public void TestIfPararmsNotCorretReturnException()
        {
            //Arrange
            string answer = "3";

            //Act

            //Assert
            Assert.That(() => { m_QuestionInt.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.NumberMustBeGreaterMin));
        }

        /// <summary>
        ///     Checks if  answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer()
        {
            //Arrange
            string answerExpected = "25";

            //Act
             string answerActual = m_QuestionInt.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual);
        }

        private QuestionInt m_QuestionInt;
    }
}
