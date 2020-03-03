using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer.Events
{
    public class Delete : IDeleteEvent<EmployerViewModel, Database.Employer>
    {
        public bool Run(EmployerViewModel model, ref IQueryable<Database.Employer> repository, IUnitOfWork unitOfWork, Response<EmployerViewModel> result, ICoreUser Employer)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.Employer>().Find(model.Id); unitOfWork.With<Database.Employer>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

