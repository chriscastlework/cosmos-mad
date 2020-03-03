using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Candidate.Mappers;
using CustomLogic.Services.Candidate.Models;

namespace CustomLogic.Services.Candidate.Events
{
    public class Save : IInsertEvent<CandidateViewModel>
    {

    public bool Run(CandidateViewModel model, IUnitOfWork unitOfWork, Response<CandidateViewModel> result, ICoreUser Candidate)
        {

            var newCustom = CandidateMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.Candidate>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = CandidateMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
