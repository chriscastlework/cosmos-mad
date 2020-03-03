using System;
using System.Linq;
using CustomLogic.Services.Job.Models;

namespace CustomLogic.Services.Job.Mappers
{
    public static class JobMapper
    {

        public static Database.Job MapInsertModelToDbModel(JobViewModel model, Database.Job newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.Job();
                newDomainModel.Id = Guid.NewGuid();
            }

            newDomainModel.Title = model.Title;
            newDomainModel.EmployerId = model.EmployerId;

            return newDomainModel;
        }



        public static JobViewModel MapDbModelToViewModel(Database.Job dbModel)
        {
            var viewModel = new JobViewModel();

            viewModel.Id = dbModel.Id;
            viewModel.Title = dbModel.Title;
            viewModel.EmployerId = dbModel.EmployerId;

            return viewModel;
        }


        public static IQueryable<JobViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.Job> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.Id).Select(c => new JobViewModel()
            {
                Id = c.Id,
                EmployerId = c.EmployerId,
                Title = c.Title
            });
        }
    }
}


