using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateResponse.Mappers;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse.Events
{
    public class Update : IUpdateEvent<CandidateResponseViewModel, Database.CandidateResponse>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(CandidateResponseViewModel model, ref IQueryable<Database.CandidateResponse> repository, IUnitOfWork unitOfWork, Response<CandidateResponseViewModel> result, ICoreUser CandidateResponse)
        {
            var dbModel =  repository.Single(c => c.JobId == model.JobId && c.CandidateId == model.CandidateId); // you need to be using the primary key could be composit
            var updatedDbModel = CandidateResponseMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.CandidateResponse>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = CandidateResponseMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

