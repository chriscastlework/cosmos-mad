using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill.Events
{
    public class Delete : IDeleteEvent<JobSkillViewModel, Database.JobSkill>
    {
        public bool Run(JobSkillViewModel model, ref IQueryable<Database.JobSkill> repository, IUnitOfWork unitOfWork, Response<JobSkillViewModel> result, ICoreUser JobSkill)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.JobSkill>().Find(model.JobId, model.SkillId); unitOfWork.With<Database.JobSkill>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

