using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class EmployerService : ServiceBase<EmployerViewModel, Database.Employer>
    {
        public EmployerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

