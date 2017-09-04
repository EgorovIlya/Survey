namespace QuestionnaireSurvey.Interface
{
    
    public interface ICommand
    {
        /// <summary>
        ///     Represents the command name.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        ///     Executes assigned command.
        /// </summary>
        void Execute();
    }
}