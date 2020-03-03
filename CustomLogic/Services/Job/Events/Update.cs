using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Job.Mappers;
using CustomLogic.Services.Job.Models;

namespace CustomLogic.Services.Job.Events
{
    public class Update : IUpdateEvent<JobViewModel, Database.Job>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(JobViewModel model, ref IQueryable<Database.Job> repository, IUnitOfWork unitOfWork, Response<JobViewModel> result, ICoreUser Job)
        {
            var dbModel =  repository.Single(c=>c.Id == model.Id); // you need to be using the primary key could be composit
            var updatedDbModel = JobMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.Job>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = JobMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

