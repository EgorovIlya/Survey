using System;
using Survey.Interface;

namespace Survey
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