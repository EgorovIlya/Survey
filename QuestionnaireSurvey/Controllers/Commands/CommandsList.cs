using System.Collections.Generic;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public static class CommandsList
    {
        /// <summary>
        ///     Shows help in console.
        /// </summary>
        public const string CommandHelp = "cmd: -help";

        /// <summary>
        ///     Creates new profile.
        /// </summary>
        public const string CommandNewProfile = "cmd: -new_profile";

        /// <summary>
        ///     Save profile.
        /// </summary>
        public const string CommandSave = "cmd: -save";

        /// <summary>
        ///     Goes to the question at the specified index, if profile is active.
        /// </summary>
        public const string CommandProfileGoToQuestion = "cmd: -goto_question";

        /// <summary>
        ///     Goes to the prevouse question, if profile is active.
        /// </summary>
        public const string CommandProfileGoToPrevioseQuestion = "cmd: -goto_prev_question";

        /// <summary>
        ///     Restarts profile. Clears all questions. Works, if profile is active.
        /// </summary>
        public const string CommandProfileRestart = "cmd: -restart_profile";

        /// <summary>
        ///     Gets statistics.
        /// </summary>
        public const string CommandStatistic = "cmd: -statistics";

        /// <summary>
        ///     Shows specified profile.
        /// </summary>
        public const string CommandFind = "cmd: -find";

        /// <summary>
        ///    Deletes specified profile.
        /// </summary>
        public const string CommandDelete= "cmd: -delete";

        /// <summary>
        ///    Shows all saved profiles.
        /// </summary>
        public const string CommandList = "cmd: -list";

        /// <summary>
        ///   Shows profiles that was created today.
        /// </summary>
        public const string CommandListToday = "cmd: -list_today";

        /// <summary>
        ///   Creates and save profile archive.
        /// </summary>
        public const string CommandZip = "cmd: -zip";



        /// <summary>
        ///     Exit.
        /// </summary>
        public const string CommandExit = "cmd: -exit";

        public static Dictionary<string, string> CommandsListDict = new Dictionary<string, string>()
        {
            {CommandNewProfile, "- Заполнить новую анкету"},
          
            {CommandStatistic, "- Показать статистику всех заполненных анкет"},
            {CommandSave, "- Сохранить заполненную анкету"},
            {CommandProfileGoToQuestion, "<Номер вопроса> - Вернуться к указанному вопросу (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)"},
            {CommandProfileGoToPrevioseQuestion, "- Вернуться к предыдущему вопросу (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)"},
            {CommandProfileRestart, "- Заполнить анкету заново (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)"},
            {CommandFind, "<Имя файла анкеты> - Найти анкету и показать данные анкеты в консоль"},
            {CommandDelete, "<Имя файла анкеты> - Удалить указанную анкету"},
            {CommandList, "- Показать список названий файлов всех сохранённых анкет"},
            {CommandListToday, "- Показать список названий файлов всех сохранённых анкет, созданных сегодня"},
            {CommandZip,"<Имя файла анкеты> <Путь для сохранения архива> - Запаковать указанную анкету в архив и сохранить архив по указанному пути" },
           {CommandHelp, "- Показать список доступных команд с описанием"},
            { CommandExit, "- Выйти из приложения"},
        };

    }
}
