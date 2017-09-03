using System;
using QuestionnaireSurvey.Interface;

namespace QuestionnaireSurvey
{
    public class WriterAndReaderWorker:IWriterAndReader
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}