using System;
using System.Collections.Generic;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.Candidate.Models
{
    public class CandidateCardViewModel
    {
        public Guid CandidateId { get; set; }
        public ICollection<CandidateSkillViewModel> Skills { get; set; }
    }
}
