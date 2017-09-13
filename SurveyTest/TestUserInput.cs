using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Survey;
using Survey.Controllers.Commands;
using Survey.Interface;
using Survey.Utils;

namespace SurveyTest
{
    /// <summary>
    /// Summary description for TestSavedProfileReader
    /// </summary>
    [TestFixture]
    public class TestUserInput
    {
        [Test]
        public void TestUserInputCommandWithParametrs()
        {
            //Act
            string expectedFullText = $"{CommandsList.CommandFind} 123-22-3231";
            string expectedCommand = CommandsList.CommandFind;
            string expectedTextWhitoutCommand = "123-22-3231";
           
            //Arrange
            UserInput userInput = new UserInput(CommandsList.CommandFind,expectedFullText);

            //Assert
            Assert.AreEqual(expectedFullText, userInput.FullText,  "Полный текст не соответствует образцу.");
            Assert.AreEqual(expectedCommand, userInput.CommandName, "Текст команды не соответствует образцу.");
            Assert.AreEqual(expectedTextWhitoutCommand, userInput.TextWhitoutCommand, "Текст без команды не соответствует образцу.");
        }


        [Test]
        public void TestUserInputCommandWithoutParametrs()
        {
            //Act
            string expectedFullText = $"{CommandsList.CommandList}";
            string expectedCommand = CommandsList.CommandList;
            string expectedTextWhitoutCommand = null;

            //Arrange
            UserInput userInput = new UserInput(CommandsList.CommandList, CommandsList.CommandList);

            //Assert
            Assert.AreEqual(expectedFullText, userInput.FullText, "Полный текст не соответствует образцу.");
            Assert.AreEqual(expectedCommand, userInput.CommandName, "Текст команды не соответствует образцу.");
            Assert.AreEqual(expectedTextWhitoutCommand, userInput.TextWhitoutCommand, "Текст без команды не соответствует образцу.");
        }


        [Test]
        public void TestUserInputIncorrectCommand()
        {
            //Act
           
            //Arrange
           
            //Assert
            Assert.That(() => { UserInput userInput = new UserInput(CommandsList.CommandFind, CommandsList.CommandList); }, Throws
                .TypeOf<SurveyException>()
                .And.Message.Contain(ErrorMessages.InputTextWithOutCommand));
        }
    }
}
