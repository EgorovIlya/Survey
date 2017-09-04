using System.Collections.Generic;
using Survey.ModelsDTO;

namespace Survey.Interface
{
    public interface IProfile
    {
        /// <summary>
        ///     Represents the list of the questions.
        /// </summary>
         List<ProfileItem> Items { get; set; }

        /// <summary>
        ///     Represents id for profile.
        /// </summary>
         string ProfileId { get; set; }
    }
}