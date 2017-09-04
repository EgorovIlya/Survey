using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;

namespace QuestionnaireSurvey.ModelsDTO
{
    public class ProfileItem
    {

        #region Constructors

        public ProfileItem(ProfileQuestion question)
        {
            Question = question;
        }

        public ProfileItem(ProfileQuestion question, string answer )
        {
            Question = question;
            Answer = answer;
        }

        #endregion Constructors

        #region Public Properties

        public ProfileQuestion Question { get; set; }

        public string Answer { get; set; } = "";

        #endregion Public Properties
    }
}
