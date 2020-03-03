using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class SkillService : ServiceBase<SkillViewModel, Database.Skill>
    {
        public SkillService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

