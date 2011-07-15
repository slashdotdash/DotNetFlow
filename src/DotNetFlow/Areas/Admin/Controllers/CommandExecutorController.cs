using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DotNetFlow.Core.Infrastructure;
using FluentValidation;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using StructureMap;

namespace DotNetFlow.Areas.Admin.Controllers
{
    /// <summary>
    /// Execute the given command, serialized as JSON
    /// </summary>
    public class CommandExecutorController : Controller
    {
        private readonly ICommandService _commandService;

        public CommandExecutorController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        //
        // POST: /Admin/JsonCommand/
        [HttpPost]
        public ActionResult Execute(string commandType)
        {
            // Only allow local requests
            if (!Request.IsLocal) return new EmptyResult();

            var typeOfCommand = typeof(DomainRegistry).Assembly.GetType(string.Concat("DotNetFlow.Core.Commands.", commandType));

            using (var reader = new StreamReader(Request.InputStream))
            {
                var requestBody = reader.ReadToEnd();
                var command = DeserializeCommand(typeOfCommand, requestBody);
                var validator = GetValidatorFor(typeOfCommand);
                var validationSummary = validator.Validate(command);

                if (validationSummary.IsValid)
                {
                    _commandService.Execute(command);
                    return new JsonResult { Data = true };
                }
                
                return new JsonResult { Data = new {  validationSummary.Errors } };
            }            
        }

        private static IValidator GetValidatorFor(Type commandType)
        {
            var commandValidatorType = typeof (IValidator<>).MakeGenericType(commandType);
            return (IValidator)ObjectFactory.GetInstance(commandValidatorType);
        }

        private static IValidator<TCommand> GetValidatorFor<TCommand>()
        {
            return ObjectFactory.GetInstance<IValidator<TCommand>>();
        }

        private static CommandBase DeserializeCommand(Type typeOfCommand, string content)
        {
            var serializer = new JavaScriptSerializer();
            
            var deserializeMethod = serializer.GetType().GetMethod("Deserialize", new [] { typeof(string)} );
            var genericMethod = deserializeMethod.MakeGenericMethod(typeOfCommand);
            
            return (CommandBase)genericMethod.Invoke(serializer, new[] { content });
        }
    }
}