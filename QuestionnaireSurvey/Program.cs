using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Controllers;

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
