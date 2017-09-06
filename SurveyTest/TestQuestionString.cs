using NUnit.Framework;
using Survey.ModelsDTO.ProfileQuestions;
using Survey.Utils;

namespace SurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestQuestionString class.
    /// </summary>
    [TestFixture]
    public class TestQuestionString
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
            m_QuestionString = new QuestionString("ФИО", "Введите ФИО", 2, 20);
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when too many characters in the answer.  
        /// </summary>
        [Test]
        public void TestAnswerWithTooManyCharsReturnException()
        {
            //Arrange
            string answer = "ToLooooongTextMoreThen20Chars";

            //Act
           
            //Assert
            Assert.That(() => { m_QuestionString.CheckedAnswer(answer);  }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.MaxLenght));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer length is too small.  
        /// </summary>
        [Test]
        public void TestIfPararmsNotCorretReturnException()
        {
            //Arrange
            string answer = "S";

            //Act

            //Assert
            Assert.That(() => { m_QuestionString.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.MinLenght));
        }

        /// <summary>
        ///     Checks if  the answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer()
        {
            //Arrange
            string answerExpected = "Correct Answer";

            //Act
             string answerActual = m_QuestionString.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual);
        }

        private QuestionString m_QuestionString;
    }
}
