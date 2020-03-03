using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.EmployerResponse.Mappers;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse.Events
{
    public class Save : IInsertEvent<EmployerResponseViewModel>
    {

    public bool Run(EmployerResponseViewModel model, IUnitOfWork unitOfWork, Response<EmployerResponseViewModel> result, ICoreUser EmployerResponse)
        {

            var newCustom = EmployerResponseMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.EmployerResponse>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = EmployerResponseMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
