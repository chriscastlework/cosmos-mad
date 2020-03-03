using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Candidate.Mappers;
using CustomLogic.Services.Candidate.Models;

namespace CustomLogic.Services.Candidate.Events
{
    public class View : IViewEvent<CandidateViewModel, Database.Candidate>
    {
        public int CreatedId = 0;

        public Response<CandidateViewModel> Run(CandidateViewModel model, ref IQueryable<Database.Candidate> repository, IUnitOfWork unitOfWork, Response<CandidateViewModel> result, ICoreUser Candidate)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.Id == model.Id);

            if (itemToUpdate != null)
            {
                var newCustomResult = CandidateMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing Candidates");
            }

            return result;
        }

    }
}
