using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class CandidateSkillService : ServiceBase<CandidateSkillViewModel, Database.CandidateSkill>
    {
        public CandidateSkillService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

