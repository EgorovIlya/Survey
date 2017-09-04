using System.Collections.Generic;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;

namespace QuestionnaireSurvey.ModelsDTO
{
   public abstract class Profile:IProfile
    {
        #region Constructors

        protected Profile(List<ProfileItem> items, string id)
        {
            ProfileId = id;
            Items = items;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Represents the list of the questions.
        /// </summary>
        public List<ProfileItem> Items { get; set; }

        /// <summary>
        ///     Represents id for profile.
        /// </summary>
        public string ProfileId { get; set; }

        #endregion Public Properties
    }
}
