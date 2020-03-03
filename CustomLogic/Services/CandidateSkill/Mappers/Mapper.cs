using System.Linq;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill.Mappers
{
    public static class CandidateSkillMapper
    {

        public static Database.CandidateSkill MapInsertModelToDbModel(CandidateSkillViewModel model, Database.CandidateSkill newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.CandidateSkill();
            }

            newDomainModel.CandidateId = model.CandidateId;
            newDomainModel.SkillId = model.SkillId;
            newDomainModel.SkillLevel = model.SkillLevel;
            return newDomainModel;
        }



        public static CandidateSkillViewModel MapDbModelToViewModel(Database.CandidateSkill dbModel)
        {
            var viewModel = new CandidateSkillViewModel();

            viewModel.CandidateId = dbModel.CandidateId;
            viewModel.SkillId = dbModel.SkillId;
            viewModel.SkillLevel = dbModel.SkillLevel;
            return viewModel;
        }


        public static IQueryable<CandidateSkillViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.CandidateSkill> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.SkillId).Select(c => new CandidateSkillViewModel()
            {
                CandidateId = c.CandidateId,
                SkillId = c.SkillId,
                SkillLevel = c.SkillLevel,
            });
        }
    }
}


