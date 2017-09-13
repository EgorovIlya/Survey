using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Survey.Utils
{
    public class SavedProfileReader
    {
        public List<string> ProfileStrings { get; set; } = new List<string>();

        public DateTime DateCreation { get; set; } 

        public SavedProfileReader(TextReader savedProfile)
        {
            TextReader  reader = savedProfile ?? throw new ArgumentNullException(nameof(savedProfile));

            using (reader)
            {
                string srline;
                while ((srline = reader.ReadLine()) != null)
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
            int indexOfSeparator = answer.IndexOf(separator, StringComparison.Ordinal);

            if (indexOfSeparator == -1)
                return "";

            int indexOfAnserBegin = indexOfSeparator + 2;
            int delta = lenght - indexOfAnserBegin;

            return answer.Substring(indexOfAnserBegin, delta);
        }
    }
}
