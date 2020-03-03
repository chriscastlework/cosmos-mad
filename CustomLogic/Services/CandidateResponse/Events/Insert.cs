using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateResponse.Mappers;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse.Events
{
    public class Save : IInsertEvent<CandidateResponseViewModel>
    {

    public bool Run(CandidateResponseViewModel model, IUnitOfWork unitOfWork, Response<CandidateResponseViewModel> result, ICoreUser CandidateResponse)
        {

            var newCustom = CandidateResponseMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.CandidateResponse>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = CandidateResponseMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
