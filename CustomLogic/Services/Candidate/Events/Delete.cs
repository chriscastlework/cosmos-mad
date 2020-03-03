using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Candidate.Models;

namespace CustomLogic.Services.Candidate.Events
{
    public class Delete : IDeleteEvent<CandidateViewModel, Database.Candidate>
    {
        public bool Run(CandidateViewModel model, ref IQueryable<Database.Candidate> repository, IUnitOfWork unitOfWork, Response<CandidateViewModel> result, ICoreUser Candidate)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.Candidate>().Find(model.Id); unitOfWork.With<Database.Candidate>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

