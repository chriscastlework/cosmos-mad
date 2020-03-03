using System;
using System.Linq;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer.Mappers
{
    public static class EmployerMapper
    {

        public static Database.Employer MapInsertModelToDbModel(EmployerViewModel model, Database.Employer newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.Employer();
                newDomainModel.Id = Guid.NewGuid();
            }

            newDomainModel.Name = model.Name;
            newDomainModel.UserId = model.UserId;
            return newDomainModel;
        }



        public static EmployerViewModel MapDbModelToViewModel(Database.Employer dbModel)
        {
            var viewModel = new EmployerViewModel();

            viewModel.Id = dbModel.Id;
            viewModel.Name = dbModel.Name;
            viewModel.UserId = dbModel.UserId;

            return viewModel;
        }


        public static IQueryable<EmployerViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.Employer> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.Id).Select(c => new EmployerViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                UserId = c.UserId
            });
        }
    }
}


