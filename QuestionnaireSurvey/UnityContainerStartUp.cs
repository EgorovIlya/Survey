using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using QuestionnaireSurvey.Class.Commands;
using QuestionnaireSurvey.Controllers;
using QuestionnaireSurvey.Controllers.Commands;
using QuestionnaireSurvey.Interface;
using QuestionnaireSurvey.ModelsDTO;

namespace QuestionnaireSurvey
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