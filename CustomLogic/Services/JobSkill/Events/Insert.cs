using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.JobSkill.Mappers;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill.Events
{
    public class Save : IInsertEvent<JobSkillViewModel>
    {

    public bool Run(JobSkillViewModel model, IUnitOfWork unitOfWork, Response<JobSkillViewModel> result, ICoreUser JobSkill)
        {

            var newCustom = JobSkillMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.JobSkill>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = JobSkillMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
