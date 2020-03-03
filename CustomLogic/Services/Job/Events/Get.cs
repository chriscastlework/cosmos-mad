using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Job.Mappers;
using CustomLogic.Services.Job.Models;

namespace CustomLogic.Services.Job.Events
{
    public class View : IViewEvent<JobViewModel, Database.Job>
    {
        public int CreatedId = 0;

        public Response<JobViewModel> Run(JobViewModel model, ref IQueryable<Database.Job> repository, IUnitOfWork unitOfWork, Response<JobViewModel> result, ICoreUser Job)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.Id == model.Id);

            if (itemToUpdate != null)
            {
                var newCustomResult = JobMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing Jobs");
            }

            return result;
        }

    }
}
