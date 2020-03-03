using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.EmployerResponse.Mappers;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse.Events
{
    public class View : IViewEvent<EmployerResponseViewModel, Database.EmployerResponse>
    {
        public int CreatedId = 0;

        public Response<EmployerResponseViewModel> Run(EmployerResponseViewModel model, ref IQueryable<Database.EmployerResponse> repository, IUnitOfWork unitOfWork, Response<EmployerResponseViewModel> result, ICoreUser EmployerResponse)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.JobId == model.JobId && c.CandidateId == model.CandidateId);

            if (itemToUpdate != null)
            {
                var newCustomResult = EmployerResponseMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing EmployerResponses");
            }

            return result;
        }

    }
}
