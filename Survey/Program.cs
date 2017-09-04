using Microsoft.Practices.Unity;
using Survey;
using Survey.Controllers;

namespace Survey
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
