using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Job.Models;

namespace CustomLogic.Services.Job.Events
{
    public class Delete : IDeleteEvent<JobViewModel, Database.Job>
    {
        public bool Run(JobViewModel model, ref IQueryable<Database.Job> repository, IUnitOfWork unitOfWork, Response<JobViewModel> result, ICoreUser Job)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.Job>().Find(model.Id); unitOfWork.With<Database.Job>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

