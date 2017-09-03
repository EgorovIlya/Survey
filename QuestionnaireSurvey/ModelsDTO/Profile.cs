using System.Collections.Generic;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;

namespace QuestionnaireSurvey.ModelsDTO
{
   public abstract class Profile:IProfile
    {
        public List<ProfileItem> Items { get; set; }

        public string ProfileId { get; set; }

        protected Profile(List<ProfileItem> items, string id)
        {
            ProfileId = id;
            Items = items;
        }
    }
}
