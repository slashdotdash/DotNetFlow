using System;
using System.Web.Mvc;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure
{
    public sealed class UseUnitOfWork : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public UseUnitOfWork()
        {
            _unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
        }

        public UseUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _unitOfWork.Initialize();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
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
    }
}