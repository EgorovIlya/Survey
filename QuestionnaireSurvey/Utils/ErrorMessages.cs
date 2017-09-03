using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSurvey.Utils
{
    public static class ErrorMessages
    {
        public const string CommandNotCorrect =
            @"Введнная команда некорректна. Введите правильную команду. Для просмотра списка комманд введите ""cmd: -help"".";

        public const string EmptyText = "Введенный текст не должен быть пустым!";

        // ProfileController
        public const string FirstElementWasReached = "Это первый вопрос анкеты!";
        public const string QuestionNumberWasNotSpecified = "Номер вопроса не указан!";
        public const string QuestionNumberIsNotNumber = "Введнный текст не удалось преобразовать в номер вопроса! Введите корректный номер!";
        public const string QuestionNumberMustBeBetweenMinAndMax = "Не верный номер вопроса. Введенный номер должен быть между 1 и max!";

        //QuestionString
        public const string MaxLenght = "Текст слишком длинный!";
        public const string MinLenght = "Текст слишком короткий!";

        //QuestionDate
        public const string DateFormatError = "Дата не соответствует формату ДД.ММ.ГГГГ! Введите правильную дату!";
        public const string DateMustBeLess = "Дата должна быть меньше, чем ";
        public const string DateMustBeGreater = "Дата должна быть больше, чем ";

        //QuestionValuesList
        public const string AnswerIsNotInTheList = "Ответ не совпадает ни с одним из возможных вариантов. Необходимо выбрать один из следующих вариантов ";

        //QuestionInt
        public const string IntFormatError = "Текст не является целым числом.Введите число!";
        public const string NumberMustBeLessMax = "Число должно быть меньше, чем ";
        public const string NumberMustBeGreaterMin = "Число должно быть больше, чем ";

        //QuestionPhoneNumber
        public const string PhoneNumberIsNotCorrect = "Номер телефона некорректный! Введите правильный номер телефона!";

        //Saver
        public const string ProfileIsNotCorrect = "Анкета заполнена не корректно! Заполните анкету заново!";

        //FileWorker
        public const string DirectoryNotExists = "Директория с результатами не создана!";
        public const string CommandMustBeWithParametrs = "Команда должна содержать параметры!";
        public const string FileNotExists = "Указанный файл не существует!";

        //ArchiveMaker
        public const string ParametrsNotCorrect = "Параметры указаны не верно";
        public const string ArchieveDirectoryNotExists = "Указан несуществующий путь к директории с архивом.";

        //StatisticsMaker
        public const string DataNotFound = "Нет данных!";

        //Tools
        public const string InputTextWithOutCommand = "Ошибка ввода. В введенном тексте нет заданной команды!";
    }
}
