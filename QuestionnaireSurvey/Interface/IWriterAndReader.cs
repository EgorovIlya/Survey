namespace QuestionnaireSurvey.Interface
{
    public interface IWriterAndReader
    {
        void WriteLine(string message);
        string ReadLine();
    }
}