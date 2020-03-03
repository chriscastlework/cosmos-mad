using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateSkill.Mappers;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill.Events
{
    public class Update : IUpdateEvent<CandidateSkillViewModel, Database.CandidateSkill>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(CandidateSkillViewModel model, ref IQueryable<Database.CandidateSkill> repository, IUnitOfWork unitOfWork, Response<CandidateSkillViewModel> result, ICoreUser CandidateSkill)
        {
            var dbModel =  repository.Single(c => c.SkillId == model.SkillId && c.CandidateId == model.CandidateId); // you need to be using the primary key could be composit
            var updatedDbModel = CandidateSkillMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.CandidateSkill>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = CandidateSkillMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

