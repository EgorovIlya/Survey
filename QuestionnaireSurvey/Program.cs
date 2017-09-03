using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Class;
using QuestionnaireSurvey.Class.Commands;
using QuestionnaireSurvey.Controllers;
using QuestionnaireSurvey.Controllers.Commands;
using QuestionnaireSurvey.Interface;

namespace QuestionnaireSurvey
{
    class Program
    {
        static void Main(string[] args)
        {


            using (var container = new UnityContainer())
            {
                UnityContainerStartUp.RegisterTypes(container);
                Controller controller = container.Resolve<Controller>();
                controller.Initialize();
            }
        }
    }
}
