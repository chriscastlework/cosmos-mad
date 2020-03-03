using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Candidate.Mappers;
using CustomLogic.Services.Candidate.Models;

namespace CustomLogic.Services.Candidate.Events
{
    public class Update : IUpdateEvent<CandidateViewModel, Database.Candidate>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(CandidateViewModel model, ref IQueryable<Database.Candidate> repository, IUnitOfWork unitOfWork, Response<CandidateViewModel> result, ICoreUser Candidate)
        {
            var dbModel =  repository.Single(c=>c.Id == model.Id); // you need to be using the primary key could be composit
            var updatedDbModel = CandidateMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.Candidate>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = CandidateMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

