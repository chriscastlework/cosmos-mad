using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateSkill.Mappers;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill.Events
{
    public class View : IViewEvent<CandidateSkillViewModel, Database.CandidateSkill>
    {
        public int CreatedId = 0;

        public Response<CandidateSkillViewModel> Run(CandidateSkillViewModel model, ref IQueryable<Database.CandidateSkill> repository, IUnitOfWork unitOfWork, Response<CandidateSkillViewModel> result, ICoreUser CandidateSkill)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.SkillId == model.SkillId && c.CandidateId == model.CandidateId);

            if (itemToUpdate != null)
            {
                var newCustomResult = CandidateSkillMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing CandidateSkills");
            }

            return result;
        }

    }
}
