﻿using System;
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
    /// <summary>
    ///     Represents a StatisticsMaker which shows the average age,the most popular programming language 
    /// and the  most experienced programmer.
    /// </summary>
    public class StatisticsMaker:ICommand
    {
        /// <summary>
        ///      Initializes a new instance of the StatisticsMaker.
        /// </summary>
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
                SavedProfileReader savedProfile = new SavedProfileReader(file);

                string fio = GetAnswer(savedProfile,m_FIO);

                string ageString = GetAnswer(savedProfile, m_BirthDay);

                if (!string.IsNullOrEmpty(ageString))
                    Ages(ageString);

                string langString = GetAnswer(savedProfile, m_Language);
              
                if (!string.IsNullOrEmpty(langString))
                    m_Languages.Add(langString);

                string expString = GetAnswer(savedProfile, m_Years);

                if (!string.IsNullOrEmpty(expString))
                    SetMaxExp(expString, fio);
            }
        }

        private string GetAnswer(SavedProfileReader savedProfile, string questionName)
        {
            string profileString = savedProfile.ProfileStrings.FirstOrDefault(n => n.Contains(questionName));

            if (!string.IsNullOrEmpty(profileString))
                return  savedProfile.GetAnswerFromSavedProfile(profileString, SurveyConst.Separator);

            return "";
        }

        /// <summary>
        ///     Sets the years of max experience and the name of the programmer.
        /// </summary>
        /// <param name="exp">the string with experience</param>
        /// <param name="fio">the name</param>
        private void SetMaxExp(string exp, string fio)
        {
            int expInt;

            if (!string.IsNullOrEmpty(fio) && int.TryParse(exp, out expInt))
            {
                if (expInt > m_MaxExp)
                {
                    m_MaxExp = expInt;
                    m_FIOmaxExp = fio;
                }
            }
        }

        /// <summary>
        ///     Displays statistics.
        /// </summary>
        private void ShowStat()
        {
            string[] vales = new[]
            {
                 SurveyConst.OutputMessageAverageAge
                ,SurveyConst.OutputMessageProgrammingLang
                ,SurveyConst.OutputMessageMaxExpPerson
            };

            foreach (var vale in vales)
            {
                WriteResult(vale);
            }
        }

        /// <summary>
        ///     Writes the results.
        /// </summary>
        /// <param name="value"></param>
        private void WriteResult(string value)
        {
            if (value == SurveyConst.OutputMessageAverageAge)
                WriteAge();
            else if (value == SurveyConst.OutputMessageProgrammingLang)
                WriteProgrammingLang();
            else if (value == SurveyConst.OutputMessageMaxExpPerson)
                WriteMaxExpPerson();
        }

        /// <summary>
        ///     Writes the average age.
        /// </summary>
        private void WriteAge()
        {
            string value = ErrorMessages.DataNotFound;
            if (m_Ages.Any())
            {
                var averageAge = (int)Math.Round(m_Ages.Average(n => n), 0);
                value =$"{averageAge} {averageAge.GetRussianYears()}";
            }
            WriterAndReaderWorker.WriteLine($"{SurveyConst.OutputMessageAverageAge}{SurveyConst.Separator}{value}");
        }
        
        /// <summary>
        ///     Writes the most experienced programmer.
        /// </summary>
        private void WriteProgrammingLang()
        {
            string value = ErrorMessages.DataNotFound;

            if (m_Languages.Any())
            {
                value = m_Languages.GroupBy(v => v)
                    .Select(s => new { s, count = s.Count() })
                    .OrderByDescending(n => n.count)
                    .First()
                    .s.Key;
            }
            WriterAndReaderWorker.WriteLine($"{SurveyConst.OutputMessageProgrammingLang}{SurveyConst.Separator}{value}");
        }

        /// <summary>
        ///     Writes the most popular language.
        /// </summary>
        private void WriteMaxExpPerson()
        {
            string value = string.IsNullOrEmpty(m_FIOmaxExp) ? ErrorMessages.DataNotFound : m_FIOmaxExp;
            WriterAndReaderWorker.WriteLine($"{SurveyConst.OutputMessageMaxExpPerson}{SurveyConst.Separator}{value}");
        }

        /// <summary>
        ///     Gets the age from the specified string.
        /// </summary>
        /// <param name="srline">the specified string</param>
        /// <param name="ages">list of ages</param>
        private void Ages(string srline)
        {
            DateTime dateBirthday;

            if (!DateTime.TryParseExact(srline, SurveyConst.FormatDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateBirthday))
                return;

            int year = DateTime.Now.Year - dateBirthday.Year;

            if (dateBirthday.AddYears(year) > DateTime.Now)
                year--;

            m_Ages.Add(year);
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

    }
}
