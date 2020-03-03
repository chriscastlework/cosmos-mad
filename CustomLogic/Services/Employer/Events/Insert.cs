using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Employer.Mappers;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer.Events
{
    public class Save : IInsertEvent<EmployerViewModel>
    {

    public bool Run(EmployerViewModel model, IUnitOfWork unitOfWork, Response<EmployerViewModel> result, ICoreUser Employer)
        {

            var newCustom = EmployerMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.Employer>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = EmployerMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
