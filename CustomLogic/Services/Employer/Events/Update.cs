using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Employer.Mappers;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer.Events
{
    public class Update : IUpdateEvent<EmployerViewModel, Database.Employer>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(EmployerViewModel model, ref IQueryable<Database.Employer> repository, IUnitOfWork unitOfWork, Response<EmployerViewModel> result, ICoreUser Employer)
        {
            var dbModel =  repository.Single(c=>c.Id == model.Id); // you need to be using the primary key could be composit
            var updatedDbModel = EmployerMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.Employer>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = EmployerMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

