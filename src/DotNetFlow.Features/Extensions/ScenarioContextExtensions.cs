using System;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Extensions
{
    internal static class ScenarioContextExtensions
    {
        public static TReturn GetOrDefault<TReturn>(this SpecFlowContext context, Func<TReturn> theDefault) where TReturn : class
        {            
            if (!context.ContainsKey(typeof(TReturn).FullName))
            {
                var created = theDefault();
                context.Set(created);
            }

            return context.Get<TReturn>();
        }

        /// <summary>
        /// Remove the given object from the context by executing the given action
        /// </summary>
        public static void Dispose<TDisposing>(this SpecFlowContext context, Action<TDisposing> disposing) where TDisposing : class
        {
            if (context.ContainsKey(typeof(TDisposing).FullName))
            {
                disposing(context.Get<TDisposing>());
                context.Remove(typeof(TDisposing).FullName);
            }
        }
    }
}