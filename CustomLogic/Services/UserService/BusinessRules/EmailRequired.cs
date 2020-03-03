using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.BL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.BusinessRules
{
    public class EmailRequired : IInsertRule<UserViewModel>
    {
        public bool Run(UserViewModel model, IUnitOfWork unitOfWork, Response<UserViewModel> result, ICoreUser user)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                result.Messages.Add(new Message
                {
                    MessageText = "Email: Required",
                    SeverityLevel = "ErrorMessage"
                });
                return false;
            }
           
            return true;
        }
    };
}
