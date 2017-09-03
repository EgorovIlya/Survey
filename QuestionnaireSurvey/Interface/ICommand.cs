namespace QuestionnaireSurvey.Interface
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}