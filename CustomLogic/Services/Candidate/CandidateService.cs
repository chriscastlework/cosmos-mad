using System;
using System.Linq;
using System.Threading.Tasks;
using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Candidate.Models;
using CustomLogic.Services.CandidateSkill.Models;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.Candidate
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class CandidateService : ServiceBase<CandidateViewModel, Database.Candidate>
    {
        public CandidateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<NgTable<CandidateCardViewModel>> GetRelevantCandidatesForJob(Guid jobId, UserViewModel userViewModel)
        {
            NgTable <CandidateCardViewModel> result = new NgTable<CandidateCardViewModel>();

            var skillIds = this.UnitOfWork.With<Database.JobSkill>().Where(c=>c.JobId == jobId).Select(c=>c.SkillId).ToArray();

            var queryResult = this.UnitOfWork.With<Database.Candidate>()
                .Where(c =>
                    (!c.EmployerResponse.Any() || c.EmployerResponse.All(cr => cr.JobId != jobId)) &&
                    c.CandidateSkill.Any(cc=> skillIds.Contains(cc.SkillId)))
                .Select(c => new CandidateCardViewModel
                {
                    CandidateId = c.Id,
                    Skills = c.CandidateSkill.Select(cc => new CandidateSkillViewModel
                    {
                        SkillId = cc.SkillId,
                        SkillLevel = cc.SkillLevel,
                        Skill = new Database.Skill
                        {
                            SkillName = cc.Skill.SkillName
                        }
                    }).ToArray()
                })
                .Take(1000)
                .ToList();

            result.Data = queryResult.OrderByDescending(o => o.Skills.Count(c => skillIds.Contains(c.SkillId))).ToList();

            result.Success = true;

            return result;
        }
    }
}

