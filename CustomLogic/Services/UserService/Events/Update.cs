using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Mappers;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.Events
{
    public class Update : IUpdateEvent<UserViewModel, User>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(UserViewModel model, ref IQueryable<User> repository, IUnitOfWork unitOfWork, Response<UserViewModel> result, ICoreUser user)
        {
            var dbModel =  repository.Single(c=>c.Id == model.Id); // you need to be using the primary key could be composit
            var updatedDbModel = UsersMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<User>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = UsersMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

