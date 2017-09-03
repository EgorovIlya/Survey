using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Controllers.Commands
{
    public class StatisticsMaker:ICommand
    {
        public StatisticsMaker()
        {
            Name = CommandsList.CommandSave;
        }

        /// <summary>
        ///     Method for writting results.
        /// </summary>
        [Dependency]
        public IWriterAndReader WriterAndReaderWorker { get; set; }

        public string Name { get; }

        public void Execute()
        {
            ReadProfiles();
            ShowStat();
        }

        private void ReadProfiles()
        {
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

            if (string.IsNullOrEmpty(m_FIOmaxExp))
                sb.Append(ErrorMessages.DataNotFound);
            else
                sb.Append(m_FIOmaxExp);

            WriterAndReaderWorker.WriteLine(sb.ToString());
      }

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

        private string GetAnswerFormSavedProfile(string answer)
        {
            int lenght = answer.Length;
            int indexSeparator = answer.IndexOf(m_Separator, StringComparison.Ordinal) + 2;
            int delta = lenght - indexSeparator;

            if (indexSeparator > lenght)
                return "";

            return answer.Substring(indexSeparator, delta);
        }

        List<int> m_Ages = new List<int>();
        List<string> m_Languages = new List<string>();
        string m_FIOmaxExp;
        private int m_MaxExp;

        private string m_BirthDay = "Дата рождения";
        private string m_Language = "Любимый язык программирования";
        private string m_Years = "Опыт программирования на указанном языке";
        private string m_FIO = "ФИО";
        private string m_Separator = ": ";
    }
}
