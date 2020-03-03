using System;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.Job.Models
{
    public class JobCardViewModel
    {
        public Guid JobId { get; set; }
        public CandidateSkillViewModel[] Skills { get; set; }
    }
}
