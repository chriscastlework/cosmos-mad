using System.Linq;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill.Mappers
{
    public static class JobSkillMapper
    {

        public static Database.JobSkill MapInsertModelToDbModel(JobSkillViewModel model, Database.JobSkill newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.JobSkill();
            }

            newDomainModel.SkillId = model.SkillId;
            newDomainModel.JobId = model.JobId;
            return newDomainModel;
        }



        public static JobSkillViewModel MapDbModelToViewModel(Database.JobSkill dbModel)
        {
            var viewModel = new JobSkillViewModel();

            viewModel.SkillId = dbModel.SkillId;
            viewModel.JobId = dbModel.JobId;
            return viewModel;
        }


        public static IQueryable<JobSkillViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.JobSkill> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.JobId).Select(c => new JobSkillViewModel()
            {
                SkillId = c.SkillId,
                JobId = c.JobId,
            });
        }
    }
}


