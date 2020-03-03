using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse.Events
{
    public class Delete : IDeleteEvent<CandidateResponseViewModel, Database.CandidateResponse>
    {
        public bool Run(CandidateResponseViewModel model, ref IQueryable<Database.CandidateResponse> repository, IUnitOfWork unitOfWork, Response<CandidateResponseViewModel> result, ICoreUser CandidateResponse)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.CandidateResponse>().Find(model.JobId, model.CandidateId); unitOfWork.With<Database.CandidateResponse>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

