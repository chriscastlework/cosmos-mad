using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.JobSkill.Mappers;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill.Events
{
    public class Update : IUpdateEvent<JobSkillViewModel, Database.JobSkill>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(JobSkillViewModel model, ref IQueryable<Database.JobSkill> repository, IUnitOfWork unitOfWork, Response<JobSkillViewModel> result, ICoreUser JobSkill)
        {
            var dbModel =  repository.Single(c => c.JobId == model.JobId && c.SkillId == model.SkillId); // you need to be using the primary key could be composit
            var updatedDbModel = JobSkillMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.JobSkill>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = JobSkillMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

