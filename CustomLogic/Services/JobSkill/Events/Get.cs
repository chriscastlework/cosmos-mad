using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.JobSkill.Mappers;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill.Events
{
    public class View : IViewEvent<JobSkillViewModel, Database.JobSkill>
    {
        public int CreatedId = 0;

        public Response<JobSkillViewModel> Run(JobSkillViewModel model, ref IQueryable<Database.JobSkill> repository, IUnitOfWork unitOfWork, Response<JobSkillViewModel> result, ICoreUser JobSkill)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.JobId == model.JobId && c.SkillId == model.SkillId);

            if (itemToUpdate != null)
            {
                var newCustomResult = JobSkillMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing JobSkills");
            }

            return result;
        }

    }
}
