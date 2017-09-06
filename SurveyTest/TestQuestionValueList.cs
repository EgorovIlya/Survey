using System.Collections.Generic;
using NUnit.Framework;
using Survey.ModelsDTO.ProfileQuestions;
using Survey.Utils;

namespace SurveyTest
{
    /// <summary>
    ///     Tests public  methods of the TestQuestionString class.
    /// </summary>
    [TestFixture]
    public class TestQuestionValueList
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
            List<string> valuesForCheking = new List<string> {"one" ,"two", "three"};
            m_QuestionValueList = new QuestionValuesList("Пункт", "Введите пункт", valuesForCheking);
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when the answer is not in the list.  
        /// </summary>
        [Test]
        public void TestAnswerNotInListReturnException()
        {
            //Arrange
            string answer = "пять";

            //Act
           
            //Assert
            Assert.That(() => { m_QuestionValueList.CheckedAnswer(answer);  }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.AnswerIsNotInTheList));
        }

        /// <summary>
        ///     Checks that an exception  will be thrown when the answer is not in the list.  
        /// </summary>
        [Test]
        public void TestAnswerIsEmptyReturnException()
        {
            //Arrange
            string answer = "";

            //Act

            //Assert
            Assert.That(() => { m_QuestionValueList.CheckedAnswer(answer); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.AnswerIsNotInTheList));
        }

        /// <summary>
        ///     Checks if the answer is correct.  
        /// </summary>
        [Test]
        public void TestCorrectAnswerReturnAnswer()
        {
            //Arrange
            string answerExpected = "two";

            //Act
             string answerActual = m_QuestionValueList.CheckedAnswer(answerExpected);
            //Assert
            Assert.AreEqual(answerExpected, answerActual);
        }

        private QuestionValuesList m_QuestionValueList;
    }
}
