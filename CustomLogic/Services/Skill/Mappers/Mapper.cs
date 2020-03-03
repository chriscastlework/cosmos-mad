using System;
using System.Linq;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill.Mappers
{
    public static class SkillMapper
    {

        public static Database.Skill MapInsertModelToDbModel(SkillViewModel model, Database.Skill newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.Skill();
                newDomainModel.Id = Guid.NewGuid();
            }

            newDomainModel.SkillName = model.SkillName;
            newDomainModel.SkillImage = model.SkillImage;
            return newDomainModel;
        }



        public static SkillViewModel MapDbModelToViewModel(Database.Skill dbModel)
        {
            var viewModel = new SkillViewModel();

            viewModel.Id = dbModel.Id;
            viewModel.SkillName = dbModel.SkillName;
            viewModel.SkillImage = dbModel.SkillImage;

            return viewModel;
        }


        public static IQueryable<SkillViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.Skill> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.Id).Select(c => new SkillViewModel()
            {
                Id = c.Id,
                SkillName = c.SkillName,
                SkillImage = c.SkillImage,
            });
        }
    }
}


