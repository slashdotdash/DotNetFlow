using System;
using System.Web.Mvc;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure
{
    public sealed class UseUnitOfWork : ActionFilterAttribute
    {
        private IUnitOfWork _unitOfWork;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
            _unitOfWork.Initialize();
        }
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                try
                {
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            else
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Rollback();
                    _unitOfWork.Dispose();
                }
            }
        }        
    }
}