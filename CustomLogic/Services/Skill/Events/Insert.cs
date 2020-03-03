using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Skill.Mappers;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill.Events
{
    public class Save : IInsertEvent<SkillViewModel>
    {

    public bool Run(SkillViewModel model, IUnitOfWork unitOfWork, Response<SkillViewModel> result, ICoreUser Skill)
        {

            var newCustom = SkillMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.Skill>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = SkillMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
