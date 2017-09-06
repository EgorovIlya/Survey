using System.Collections.Generic;
using Survey.ModelsDTO;
using Survey.Interface;

namespace Survey.ModelsDTO
{
   public abstract class Profile:IProfile
    {
        #region Constructors

        /// <summary>
        ///      Initializes a new instance of the Profile using the specified list of the ProfileItems and id.
        /// </summary>
        /// <param name="items">the specified list of the ProfileItems</param>
        /// <param name="id">id</param>
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
