using System.IO;
using System.Linq;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using Newtonsoft.Json;
using StructureMap;

namespace DotNetFlow.Utils
{
    internal sealed class ImportCommands
    {
        public void Execute()
        {
            new DatabaseCleaner().Execute();
            Bootstrapper.Configure();

            var commandAssembly = typeof(ICommand).Assembly;
            var commandService = ObjectFactory.GetInstance<ICommandService>();

            using (var uow = ObjectFactory.GetInstance<IUnitOfWork>())
            {
                uow.Begin();

                foreach (var file in Directory.GetFiles(@"C:\src\dotnetflow\database\seed").OrderBy(file => file))
                {
                    var typeOfCommand = file.Split(' ')[1].Replace(".json", string.Empty);
                    var json = File.ReadAllText(file);

                    var command = (ICommand)JsonConvert.DeserializeObject(json, commandAssembly.GetType(string.Concat("DotNetFlow.Core.Commands.", typeOfCommand)));

                    commandService.Execute(command);
                }

                uow.Commit();
            }
        }
    }
}