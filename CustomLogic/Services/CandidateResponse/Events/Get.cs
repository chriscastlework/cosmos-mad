using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateResponse.Mappers;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse.Events
{
    public class View : IViewEvent<CandidateResponseViewModel, Database.CandidateResponse>
    {
        public int CreatedId = 0;

        public Response<CandidateResponseViewModel> Run(CandidateResponseViewModel model, ref IQueryable<Database.CandidateResponse> repository, IUnitOfWork unitOfWork, Response<CandidateResponseViewModel> result, ICoreUser CandidateResponse)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.JobId == model.JobId && c.CandidateId == model.CandidateId);

            if (itemToUpdate != null)
            {
                var newCustomResult = CandidateResponseMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing CandidateResponses");
            }

            return result;
        }

    }
}
