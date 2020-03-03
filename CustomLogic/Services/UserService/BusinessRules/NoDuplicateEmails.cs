// EXAMPLE ONLY AS THIS IS COVERED BY THE DATABASE

//using System.Linq;
//using RecruitMe.Core.Interfaces;
//using RecruitMe.Core.Interfaces.BL;
//using RecruitMe.Core.Interfaces.Models;
//using RecruitMe.Web.Logic.Core.Interfaces;
//using RecruitMe.Web.Logic.Database;
//using RecruitMe.Web.Logic.Services.UserService.Models;

//namespace RecruitMe.Web.Logic.Services.UserService.BusinessRules
//{
//    public class NoDuplicateEmails : IInsertRule<UserViewModel>
//    {
//        public bool Run(UserViewModel model, IUnitOfWork unitOfWork, Response<UserViewModel> result, ICoreUser user)
//        {
//            var otherEmails = unitOfWork.With<User>().Any(c => c.Email == model.Email);
//            if (otherEmails)
//            {
//                result.Messages.Add(new Message
//                {
//                    MessageText = "Other user with this email already exists",
//                    SeverityLevel = "Danger"
//                });
//            }
//            return !otherEmails;
//        }
//    };
//}
