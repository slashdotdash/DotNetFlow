using System;
using System.Web.Mvc;

namespace DotNetFlow.Core.Infrastructure
{
    /// <summary>
    /// Use a unit-of-work per web request, rolling back the transaction on any thrown exception
    /// </summary>
    public sealed class UseUnitOfWork : ActionFilterAttribute
    {
        // Use a Func as in ASP.NET MVC 3 filters are cached
        private readonly Func<IUnitOfWork> _unitOfWorkFactory;

        public UseUnitOfWork(Func<IUnitOfWork> unitOfWork)
        {
            _unitOfWorkFactory = unitOfWork;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _unitOfWorkFactory().Begin();
        }
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var unitOfWork = _unitOfWorkFactory();

            try
            {
                if (filterContext.Exception == null || filterContext.ExceptionHandled)
                {
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }        
    }
}