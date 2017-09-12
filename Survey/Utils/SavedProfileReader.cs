using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Survey.Interface;

namespace Survey.Utils
{
    public class SavedProfileReader
    {
        public List<string> ProfileStrings { get; set; } = new List<string>();

        public DateTime DateCreation { get; set; } 

        public SavedProfileReader(TextReader savedProfile)
        {
            m_reader = savedProfile ?? throw new ArgumentNullException(nameof(savedProfile));
            using (m_reader)
            {
                string srline;
                while ((srline = m_reader.ReadLine()) != null)
                {
                    if (srline.Contains(SurveyConst.ProfileWasCreated))
                    {
                        string dateCreationString = GetAnswerFromSavedProfile(srline, SurveyConst.Separator);

                        DateTime dateCreation;

                        if (DateTime.TryParseExact(dateCreationString, SurveyConst.FormatDate,
                            CultureInfo.CurrentCulture, DateTimeStyles.None, out dateCreation))
                        {
                            DateCreation = dateCreation;
                        }
                    }

                    ProfileStrings.Add(srline);
                }
            }
        }

        public SavedProfileReader(string fileName):this(new StreamReader(fileName))
        {
        }

        /// <summary>
        ///     Gets the clear user answer form the specified string.
        /// </summary>
        /// <param name="answer">the saved answer</param>
        /// <param name="separator">the separator</param>
        /// <returns>the answer without technical text</returns>
        public string GetAnswerFromSavedProfile(string answer, string separator)
        {
            int lenght = answer.Length;
            int indexSeparator = answer.IndexOf(separator, StringComparison.Ordinal) + 2;
            int delta = lenght - indexSeparator;

            if (indexSeparator > lenght)
                return "";

            return answer.Substring(indexSeparator, delta);
        }

        private readonly TextReader m_reader;
    }
}
