using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Mappers;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.Events
{
    public class Save : IInsertEvent<UserViewModel>
    {

    public bool Run(UserViewModel model, IUnitOfWork unitOfWork, Response<UserViewModel> result, ICoreUser user)
        {

            var newCustom = UsersMapper.MapInsertModelToDbModel(model);
            unitOfWork.With<User>().Add(newCustom);
            unitOfWork.SaveChanges();
            var newCustomResult = UsersMapper.MapDbModelToViewModel(newCustom);
            result.Body = newCustomResult;
            return true;
        }
    }
}
