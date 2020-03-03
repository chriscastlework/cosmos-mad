using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class UserService : ServiceBase<UserViewModel, User>
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

