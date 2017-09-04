using NUnit.Framework;
using Survey.ModelsDTO.ProfileQuestions;
using Survey.Utils;

namespace QuestionnaireSurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestQuestionString class.
    /// </summary>
    [TestFixture]
    public class TestQuestionPhoneNumber
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
            m_QuestionPhoneNumber = new QuestionPhoneNumber("Телефон", "Введите номер");
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when answer is not phone number.  
        /// </summary>
        [Test]
        public void TestAnswerIsNotCorrect()
        {
            //Arrange
            string answer = "OneTwoThreeFourAndMore";

            //Act
           
            //Assert
            Assert.That(() => { m_QuestionPhoneNumber.CheckedAnswer(answer);  }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.PhoneNumberIsNotCorrect));
        }


        /// <summary>
        ///     Checks that an exception  will be thrown when answer is not phone number.  
        /// </summary>
        [Test]
        public void TestAnswerIsNotCorrectInOneSimvol()
        {
            //Arrange
            string answer = "+7(777)77a777";

            //Act

            //Assert
            Assert.That(() => { m_QuestionPhoneNumber.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.PhoneNumberIsNotCorrect));
        }


        /// <summary>
        ///     Checks if  answer is not correct.  
        /// </summary>
        [Test]
        public void TestNotCorrectAnswerReturnAnswer()
        {
            //Arrange
            string answerExpected = "+7-777-777-77-77";

            //Act
            
            //Assert
         
            Assert.That(() => { m_QuestionPhoneNumber.CheckedAnswer(answerExpected); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.PhoneNumberIsNotCorrect));
        }

        /// <summary>
        ///     Checks if  answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer()
        {
            //Arrange
            string answerExpected = "+7(777)7777777";

            //Act
             string answerActual = m_QuestionPhoneNumber.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual);
        }


        /// <summary>
        ///     Checks if  answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer1()
        {
            //Arrange
            string answerExpected = "+7-777-7777777";

            //Act
            string answerActual = m_QuestionPhoneNumber.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual, $"{answerExpected} должен быть корректным!" );
        }

        /// <summary>
        ///     Checks if  answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer2()
        {
            //Arrange
            string answerExpected = "+7-777-777-7777";

            //Act
            string answerActual = m_QuestionPhoneNumber.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual, $"{answerExpected} должен быть корректным!");
        }

        /// <summary>
        ///     Checks if  answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer3()
        {
            //Arrange
            string answerExpected = "8-777-777-7777";

            //Act
            string answerActual = m_QuestionPhoneNumber.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual, $"{answerExpected} должен быть корректным!");
        }
        private QuestionPhoneNumber m_QuestionPhoneNumber;
    }
}
