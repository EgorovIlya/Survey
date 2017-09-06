using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Survey.Interface;
using Survey.Utils;

namespace Survey.Controllers.Commands
{
    public class StatisticsMaker:ICommand
    {
        public StatisticsMaker()
        {
            CommandName = CommandsList.CommandSave;
        }

        /// <summary>
        ///     Represents a method for writing results and reading user input.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        /// <summary>
        ///     Represents a command from commandList.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        ///      The ICommmand implementation. Displays statistics for all saved profiles.
        /// </summary>
        public void Execute()
        {
            ReadProfiles();
            ShowStat();
        }

        /// <summary>
        ///     Reads all saved profile.
        /// </summary>
        private void ReadProfiles()
        {
            if (!Directory.Exists(SurveyConst.DirectoryName))
                throw new SurveyException(ErrorMessages.DirectoryNotExists);

            if (!Directory.GetFiles(SurveyConst.DirectoryName).Any())
                throw new SurveyException($"{ErrorMessages.SavedProfilesNotFound} {Path.GetFullPath(SurveyConst.DirectoryName)}");

            var files = Directory.GetFiles(SurveyConst.DirectoryName);

            foreach (var file in files)
            {
                string FIO = "";
                string srline;
                using (StreamReader r = new StreamReader(file, true))
                {
                    while ((srline =r.ReadLine()) != null)
                    {
                        if (srline.Contains(m_FIO))
                        {
                            FIO = GetAnswerFormSavedProfile(srline);
                        }

                        if (srline.Contains(m_BirthDay))
                        {
                            Ages(srline, ref m_Ages);
                        }

                        if (srline.Contains(m_Language))
                        {
                            string lang = GetAnswerFormSavedProfile(srline);
                            if(!string.IsNullOrEmpty(lang))
                                m_Languages.Add(lang);
                        }

                        if (srline.Contains(m_Years))
                        {
                            string exp = GetAnswerFormSavedProfile(srline);
                            int expInt;

                            if (!string.IsNullOrEmpty(FIO) && int.TryParse(exp, out expInt))
                            {
                                if (expInt > m_MaxExp )
                                {
                                    m_MaxExp = expInt;
                                    m_FIOmaxExp = FIO;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Displays statistics.
        /// </summary>
        private void ShowStat()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SurveyConst.OutputMessageAverageAge);
            sb.Append(" ");

            if (m_Ages.Any())
            {
                var averageAge = (int) Math.Round(m_Ages.Average(n => n), 0);
                sb.Append(averageAge);
                sb.Append(" ");
                sb.Append(Tools.GetYears(averageAge));
            }
            else
            {
                 sb.Append(ErrorMessages.DataNotFound);
            }

            WriterAndReaderWorker.WriteLine(sb.ToString());
            sb.Clear();
            sb.Append(SurveyConst.OutputMessageProgrammingLang);
            sb.Append(" ");

            if (m_Languages.Any())
            {
                var lang = m_Languages.GroupBy(v => v)
                    .Select(s => new {s, count = s.Count()})
                    .OrderByDescending(n => n.count)
                    .First()
                    .s.Key;
                sb.Append(lang);
            }
            else
            {
                sb.Append(ErrorMessages.DataNotFound);
            }

            WriterAndReaderWorker.WriteLine(sb.ToString());
            sb.Clear();

            sb.Append(SurveyConst.OutputMessageMaxExpPerson);
            sb.Append(" ");

            sb.Append(string.IsNullOrEmpty(m_FIOmaxExp) ? ErrorMessages.DataNotFound : m_FIOmaxExp);

            WriterAndReaderWorker.WriteLine(sb.ToString());
        }
        
        /// <summary>
        ///     Gets the age from the specified string.
        /// </summary>
        /// <param name="srline">the specified string</param>
        /// <param name="ages">list of ages</param>
        private void Ages(string srline, ref List<int> ages)
        {
           
            string birthday = GetAnswerFormSavedProfile(srline);

            DateTime dateBirthday;

            if (!DateTime.TryParseExact(birthday, SurveyConst.FormatDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateBirthday))
                return;

            int year = DateTime.Now.Year - dateBirthday.Year;

            if (dateBirthday.AddYears(year) > DateTime.Now)
                year--;

            ages.Add(year);
        }

        /// <summary>
        ///     Gets the clear user answer form the specified string.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        private string GetAnswerFormSavedProfile(string answer)
        {
            int lenght = answer.Length;
            int indexSeparator = answer.IndexOf(m_Separator, StringComparison.Ordinal) + 2;
            int delta = lenght - indexSeparator;

            if (indexSeparator > lenght)
                return "";

            return answer.Substring(indexSeparator, delta);
        }

        /// <summary>
        ///     The list of ages.
        /// </summary>
        List<int> m_Ages = new List<int>();
        
        /// <summary>
        ///     The list of the programming languages.
        /// </summary>
        List<string> m_Languages = new List<string>();

        /// <summary>
        ///     The name of the user with maximum experience.
        /// </summary>
        string m_FIOmaxExp;

        /// <summary>
        ///     Number of years of maximum experience.
        /// </summary>
        private int m_MaxExp;

        /// <summary>
        ///     The name of the question about the birthday.
        /// </summary>
        private string m_BirthDay = "Дата рождения";

        /// <summary>
        ///     The name of the question about the programming language.
        /// </summary>
        private string m_Language = "Любимый язык программирования";

        /// <summary>
        ///     The name of the question about  experience.
        /// </summary>
        private string m_Years = "Опыт программирования на указанном языке";

        /// <summary>
        ///     The name of the question about the name.
        /// </summary>
        private string m_FIO = "ФИО";

        /// <summary>
        ///     The separator.
        /// </summary>
        private string m_Separator = ": ";
    }
}
