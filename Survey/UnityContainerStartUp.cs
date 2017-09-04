using Microsoft.Practices.Unity;
using Survey.Controllers.Commands;
using Survey.Interface;
using Survey.ModelsDTO;

namespace Survey
{
    public class UnityContainerStartUp
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICommand, EmptyCommand>()
                .RegisterType<ICommand, Helper>(CommandsList.CommandHelp)
                .RegisterType<ICommand, ProfileConroller>(CommandsList.CommandNewProfile)
                .RegisterType<ICommand, Saver>(CommandsList.CommandSave)
                .RegisterType<ICommand, Exiter>(CommandsList.CommandExit)
                .RegisterType<ICommand, ArchiveMaker>(CommandsList.CommandZip)
                .RegisterType<ICommand, StatisticsMaker>(CommandsList.CommandStatistic)
                .RegisterType<ICommand, FileWorker>(CommandsList.CommandFind)
                .RegisterType<ICommand, FileWorker>(CommandsList.CommandDelete)
                .RegisterType<ICommand, FileWorker>(CommandsList.CommandList)
                .RegisterType<ICommand, FileWorker>(CommandsList.CommandListToday)
                ;

            container.RegisterType<IProfile, ProfileProgramming>();

            container.RegisterType<IWriterAndReader, WriterAndReaderWorker>();
        }
    }
}