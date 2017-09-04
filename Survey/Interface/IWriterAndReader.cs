namespace Survey.Interface
{
    public interface IWriterAndReader
    {
        /// <summary>
        ///     Shows message. 
        /// </summary>
        /// <param name="message">specified message</param>
        void WriteLine(string message);

        /// <summary>
        ///     Receives message.
        /// </summary>
        /// <returns> received message</returns>
        string ReadLine();
    }
}