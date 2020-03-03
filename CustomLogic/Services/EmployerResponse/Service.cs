using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class EmployerResponseService : ServiceBase<EmployerResponseViewModel, Database.EmployerResponse>
    {
        public EmployerResponseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

