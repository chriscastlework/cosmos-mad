using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class JobSkillService : ServiceBase<JobSkillViewModel, Database.JobSkill>
    {
        public JobSkillService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

