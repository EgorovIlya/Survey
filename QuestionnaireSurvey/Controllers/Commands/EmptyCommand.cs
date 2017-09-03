using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.Utils;

namespace QuestionnaireSurvey.Class.Commands
{
    public class EmptyCommand:ICommand
    {
        #region Constructors

        public EmptyCommand()
        {
            Name = "";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Returns name of command.
        /// </summary>
        public string Name { get; }

        #endregion Properties

        #region Public Methods

        public void Execute()
        {
          throw new SurveyException(ErrorMessages.CommandNotCorrect);
        }

        #endregion Public Methods
    }
}
