using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure.Eventing
{
    internal sealed class RegisterEventHandlersInAssembly
    {
        private readonly EventDispatcher _dispatcher;
        private readonly MethodInfo _registerHandler;
        private readonly List<Assembly> _lookInTheseAssemblies;

        public RegisterEventHandlersInAssembly(EventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _registerHandler = _dispatcher.GetType().GetMethod("RegisterHandler");
            _lookInTheseAssemblies = new List<Assembly>();
        }

        public void IncludeAssembly(Assembly assembly)
        {
            _lookInTheseAssemblies.Add(assembly);
        }

        public void RegisterHandlers()
        {
            _lookInTheseAssemblies.ForEach(RegisterEventHandlers);
        }

        /// <summary>
        /// Locate and register all event handling classes that implement the interface IEventHandler<TEvent>
        /// </summary>
        /// <param name="assembly">Assembly to search for types in</param>
        private void RegisterEventHandlers(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var implementedInterface in type.GetInterfaces().Where(IsEventHandler))
                {
                    var typeofEventHandler = type;
                    var typeofEvent = implementedInterface.GetGenericArguments()[0];

                    var handlerAction = CreateHandlerAction(typeofEvent, typeofEventHandler);   
                 
                    RegisterHandlerForEvent(typeofEvent, handlerAction);                    
                }
            }
        }
        
        private static bool IsEventHandler(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (IEventHandler<>);
        }

        private static object CreateHandlerAction(Type typeofEvent, Type typeofEventHandler)
        {
            var converterType = typeof(ConvertableEventHandler<>).MakeGenericType(new[] { typeofEvent });
            var convertHandlerCtor = converterType.GetConstructor(new[] { typeof(Type) });
            var convertedHandler = convertHandlerCtor.Invoke(new object[] { typeofEventHandler });

            return converterType.GetMethod("CreateHandlerAction").Invoke(convertedHandler, null);
        }

        private void RegisterHandlerForEvent(Type typeofEvent, object handlerAction)
        {
            var genericRegistration = _registerHandler.MakeGenericMethod(typeofEvent);

            genericRegistration.Invoke(_dispatcher, new[] { typeofEvent, handlerAction });
        }
    }

    internal sealed class ConvertableEventHandler<TEvent> where TEvent : IDomainEvent
    {
        private readonly Type _typeofEventHandler;

        public ConvertableEventHandler(Type typeofEventHandler)
        {
            _typeofEventHandler = typeofEventHandler;
        }

        public Action<TEvent> CreateHandlerAction()
        {
            return Handle;
        }

        private void Handle(TEvent evnt)
        {
            var handler = (IEventHandler<TEvent>)ObjectFactory.GetInstance(_typeofEventHandler);
            handler.Handle(evnt);
        }
    }
}