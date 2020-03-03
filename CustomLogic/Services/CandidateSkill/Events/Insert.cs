using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateSkill.Mappers;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill.Events
{
    public class Save : IInsertEvent<CandidateSkillViewModel>
    {

    public bool Run(CandidateSkillViewModel model, IUnitOfWork unitOfWork, Response<CandidateSkillViewModel> result, ICoreUser CandidateSkill)
        {

            var newCustom = CandidateSkillMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<Database.CandidateSkill>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = CandidateSkillMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
