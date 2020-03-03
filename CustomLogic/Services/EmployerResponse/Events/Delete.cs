using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse.Events
{
    public class Delete : IDeleteEvent<EmployerResponseViewModel, Database.EmployerResponse>
    {
        public bool Run(EmployerResponseViewModel model, ref IQueryable<Database.EmployerResponse> repository, IUnitOfWork unitOfWork, Response<EmployerResponseViewModel> result, ICoreUser EmployerResponse)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.EmployerResponse>().Find(model.CandidateId, model.JobId);
            unitOfWork.With<Database.EmployerResponse>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

