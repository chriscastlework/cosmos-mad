using System;
using System.Linq;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.Mappers
{
    public static class UsersMapper
    {

        public static User MapInsertModelToDbModel(UserViewModel model, User newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new User();
                newDomainModel.Id = Guid.NewGuid();
            }

            newDomainModel.Email = model.Email;
            newDomainModel.FirstName = model.FirstName;
            newDomainModel.LastName = model.LastName;
            return newDomainModel;
        }



        public static UserViewModel MapDbModelToViewModel(User dbModel)
        {
            var viewModel = new UserViewModel();

            viewModel.Id = dbModel.Id;
            viewModel.Email = dbModel.Email;
            viewModel.FirstName = dbModel.FirstName;
            viewModel.LastName = dbModel.LastName;

            return viewModel;
        }


        public static IQueryable<UserViewModel> MapDbModelQueryToViewModelQuery(IQueryable<User> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.Id).Select(c => new UserViewModel()
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
            });
        }
    }
}


