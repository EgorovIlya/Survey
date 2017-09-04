using NUnit.Framework;
using Survey.Controllers.Commands;
using Survey.Utils;

namespace SurveyTest
{
    /// <summary>
    /// Summary description for TestTools
    /// </summary>
    [TestFixture]
    public class TestTools
    {
        /// <summary>
        ///     Checks that "год" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf1EndedReturnYear()
        {
            //Arrange
            string expected = "год";
            int years = 101;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "лет" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf11EndedReturnYears()
        {
            //Arrange
            string expected = "лет";
            int years = 111;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "года" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf2Or3EndedReturnYear()
        {
            //Arrange
            string expected = "года";
            int years = 52;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "лет" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf12EndedReturnYears()
        {
            //Arrange
            string expected = "лет";
            int years = 12;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "лет" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf13EndedReturnYears()
        {
            //Arrange
            string expected = "лет";
            int years = 1013;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "лет" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf14EndedReturnYears()
        {
            //Arrange
            string expected = "лет";
            int years = 1014;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "года" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf3EndedReturnYears()
        {
            //Arrange
            string expected = "года";
            int years = 63;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "года" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf4EndedReturnYears()
        {
            //Arrange
            string expected = "года";
            int years = 1234;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that "лет" is returned.
        /// </summary>
        [Test]
        public void TestGetYearsIf5EndedReturnYears()
        {
            //Arrange
            string expected = "лет";
            int years = 12345;

            //Act
            var actual = Tools.GetYears(years);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that returns a text without command, if the text is correct.
        /// </summary>
        [Test]
        public void GetTextWithoutCommandIfTextIsCorrect()
        {
            //Arrange
            string expected = "123456";
            string text = $"{CommandsList.CommandFind} {expected}";

            //Act
            var actual = Tools.GetTextWithoutCommand(text, CommandsList.CommandFind);

            //Assert
            Assert.AreEqual(expected, actual, $@"Ошибка! Должен быть ""{expected}"", отразилось ""{actual}""");
        }

        /// <summary>
        ///     Checks that returns a text without command, if the text is correct.
        /// </summary>
        [Test]
        public void GetTextWithoutCommandIfTextIsNotCorrect()
        {
            //Arrange
            string text = $"123456798 1321546545666";

            //Act

            //Assert
            Assert.That(() => { Tools.GetTextWithoutCommand(text, CommandsList.CommandFind); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.InputTextWithOutCommand));
        }

        /// <summary>
        ///     Checks that returns a text without command, if the text is correct.
        /// </summary>
        [Test]
        public void GetIfTextIsNotCorrectWithoutParams()
        {
            //Arrange
            string text = $"{CommandsList.CommandFind}";

            //Act

            //Assert
            Assert.That(() => { Tools.GetTextWithoutCommand(text, CommandsList.CommandFind); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.EqualTo(ErrorMessages.CommandMustBeWithParametrs));
        }
    }
}
