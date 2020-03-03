using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.Events
{
    public class Delete : IDeleteEvent<UserViewModel, User>
    {
        public bool Run(UserViewModel model, ref IQueryable<User> repository, IUnitOfWork unitOfWork, Response<UserViewModel> result, ICoreUser user)
        {
            // Todo change id for the tables PK
            var customToRemove = unitOfWork.With<User>().Find(model.Id); unitOfWork.With<User>().Remove(customToRemove);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}

