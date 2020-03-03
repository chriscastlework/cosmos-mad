using System.Linq;
using System.Threading.Tasks;
using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateSkill.Models;
using CustomLogic.Services.Job.Models;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.Job
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class JobService : ServiceBase<JobViewModel, Database.Job>
    {
        public JobService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<NgTable<JobCardViewModel>> GetRelevantJobsForCandidate(UserViewModel userViewModel)
        {
            NgTable<JobCardViewModel> result = new NgTable<JobCardViewModel>();

            var skillIds = UnitOfWork.With<Database.Candidate>().Find(userViewModel.CandidateId).CandidateSkill.Select(cc => cc.SkillId).ToList();

            var queryResult = UnitOfWork.With<Database.Job>()
                .Where(c =>
                    (!c.CandidateResponse.Any() || c.CandidateResponse.All(cr => cr.CandidateId != userViewModel.CandidateId)) &&
                    c.JobSkill.Any(cc => skillIds.Contains(cc.SkillId)))
                .Select(c => new JobCardViewModel
                {
                    JobId = c.Id,
                    Skills = c.JobSkill.Select(cc => new CandidateSkillViewModel
                    {
                        SkillId = cc.SkillId,
                        Skill = new Database.Skill
                        {
                            SkillName = cc.Skill.SkillName
                        }
                    }).ToArray()
                })
                .Take(1000)
                .ToArray();

            result.Data = queryResult.OrderByDescending(o => o.Skills.Count(c => skillIds.Contains(c.SkillId))).ToList();

            result.Success = true;

            return result;
        }
    }
}

