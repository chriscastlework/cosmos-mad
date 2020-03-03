using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Job.Mappers;
using CustomLogic.Services.Job.Models;

namespace CustomLogic.Services.Job.Events
{
    public class Save : IInsertEvent<JobViewModel>
    {

    public bool Run(JobViewModel model, IUnitOfWork unitOfWork, Response<JobViewModel> result, ICoreUser Job)
        {

            var newCustom = JobMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.Job>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = JobMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
