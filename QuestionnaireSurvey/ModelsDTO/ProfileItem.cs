using QuestionnaireSurvey.ModelsDTO.ProfileQuestions;

namespace QuestionnaireSurvey.ModelsDTO
{
    public class ProfileItem
    {
        public ProfileQuestion Question { get; set; }

        public string Answer { get; set; } = "";

        public ProfileItem(ProfileQuestion question)
        {
            Question = question;
        }

        public ProfileItem(ProfileQuestion question, string answer )
        {
            Question = question;
            Answer = answer;
        }
    }
}
