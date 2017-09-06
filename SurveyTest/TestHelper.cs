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
    ///       Tests public  methods of the Helper class.
    /// </summary>
    [TestFixture]
    public class TestHelper
    {
        [OneTimeSetUp]
        public void Init()
        {
            m_container = new UnityContainer();
            UnityContainerStartUp.RegisterTypes(m_container);
        }

        /// <summary>
        ///     Checks the actions when the "Save" command is entered.
        /// </summary>
        [Test]
        public void TestSetCommandHelpReturnHelper()
        {
            //Arrange
            List<string> helpExpected = new List<string>
            {
                 "cmd: -new_profile - Заполнить новую анкету"
                ,"cmd: -statistics - Показать статистику всех заполненных анкет"
                ,"cmd: -save - Сохранить заполненную анкету"
                ,"cmd: -goto_question <Номер вопроса> - Вернуться к указанному вопросу (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)"
                ,"cmd: -goto_prev_question - Вернуться к предыдущему вопросу (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)"
                ,"cmd: -restart_profile - Заполнить анкету заново (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)"
                ,"cmd: -find <Имя файла анкеты> - Найти анкету и показать данные анкеты в консоль"
                ,"cmd: -delete <Имя файла анкеты> - Удалить указанную анкету"
                ,"cmd: -list - Показать список названий файлов всех сохранённых анкет"
                ,"cmd: -list_today - Показать список названий файлов всех сохранённых анкет, созданных сегодня"
                ,"cmd: -zip <Имя файла анкеты> <Путь для сохранения архива> - Запаковать указанную анкету в архив и сохранить архив по указанному пути"
                ,"cmd: -help - Показать список доступных команд с описанием"
                ,"cmd: -exit - Выйти из приложения"

            };
            
            //Act
            List<string> helpResived = new List<string>();
            var consoleMock = new Mock<IWriterAndReader>();
            consoleMock.Setup(c => c.WriteLine(It.IsAny<string>())).Callback((string s) => helpResived.Add(s));
            var helperMoq = m_container.Resolve<ICommand>(CommandsList.CommandHelp, new PropertyOverride(SurveyConst.WriterAndReaderWorker, consoleMock.Object));
            helperMoq.Execute();

            //Assert
            CollectionAssert.AreEqual(helpExpected, helpResived);
        }

        private IUnityContainer m_container;
     
    }
}
