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
    }

}
