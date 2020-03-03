using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Mappers;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.Events
{
    public class View : IViewEvent<UserViewModel, User>
    {
        public int CreatedId = 0;

        public Response<UserViewModel> Run(UserViewModel model,ref IQueryable<User> repository, IUnitOfWork unitOfWork, Response<UserViewModel> result, ICoreUser user)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.Id == model.Id);

            if (itemToUpdate != null)
            {
                var newCustomResult = UsersMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing Users");
            }

            return result;
        }

    }
}
