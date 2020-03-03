using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Skill.Mappers;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill.Events
{
    public class Update : IUpdateEvent<SkillViewModel, Database.Skill>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(SkillViewModel model, ref IQueryable<Database.Skill> repository, IUnitOfWork unitOfWork, Response<SkillViewModel> result, ICoreUser Skill)
        {
            var dbModel =  repository.Single(c=>c.Id == model.Id); // you need to be using the primary key could be composit
            var updatedDbModel = SkillMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.Skill>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = SkillMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

