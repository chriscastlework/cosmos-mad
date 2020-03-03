using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill.Events
{
    public class Delete : IDeleteEvent<CandidateSkillViewModel, Database.CandidateSkill>
    {
        public bool Run(CandidateSkillViewModel model, ref IQueryable<Database.CandidateSkill> repository, IUnitOfWork unitOfWork, Response<CandidateSkillViewModel> result, ICoreUser CandidateSkill)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<Database.CandidateSkill>().Find(model.CandidateId, model.SkillId);
            unitOfWork.With<Database.CandidateSkill>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

