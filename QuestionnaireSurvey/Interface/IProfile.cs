using System.Collections.Generic;
using QuestionnaireSurvey.ModelsDTO;

namespace QuestionnaireSurvey.Interface
{
    public interface IProfile
    {
         List<ProfileItem> Items { get; set; }

         string ProfileId { get; set; }
    }
}