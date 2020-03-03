using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill.Events
{
    public class Delete : IDeleteEvent<SkillViewModel, Database.Skill>
    {
        public bool Run(SkillViewModel model, ref IQueryable<Database.Skill> repository, IUnitOfWork unitOfWork, Response<SkillViewModel> result, ICoreUser Skill)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.Skill>().Find(model.Id); unitOfWork.With<Database.Skill>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

