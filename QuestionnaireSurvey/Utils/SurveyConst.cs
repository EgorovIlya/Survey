using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSurvey.Controllers.Commands;

namespace QuestionnaireSurvey.Utils
{
    public static class SurveyConst
    {
        public const string StartMessages = "Выберите действие:";
        public const int MaximumLifeSpan = 125;
        public const string FormatDate = "dd.MM.yyyy";
        public const string DirectoryName = "Анкеты";
        public const string SaveProfile = "Анкета заполнена, но не сохранена! Для сохранения введите команду ";

        //StatisticsMaker
        public const string OutputMessageMaxExpPerson = "Самый опытный программист:";
        public const string OutputMessageAverageAge = "Средний возраст всех опрошенных:";
        public const string OutputMessageProgrammingLang = "Самый популярный язык программирования:";

        //Constr parametrs name

        /// <summary>
        ///     Name of property. Writer and reader.
        /// </summary>
        public const string WriterAndReaderWorker = "WriterAndReaderWorker";

       
        /// <summary>
        ///     Name of property. Working profile.
        /// </summary>
        public const string WorkingProfile = "WorkingProfile";

        /// <summary>
        ///     Constructor parametr name. The name of the command (from CommandDict).
        /// </summary>
        public const string CommandName = "commandName";

        /// <summary>
        ///      Constructor parametr name. User input.
        /// </summary>
        public const string UserInput = "userInput";
    }

}
