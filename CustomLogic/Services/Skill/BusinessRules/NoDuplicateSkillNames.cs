// EXAMPLE ONLY AS THIS IS COVERED IN THE DATABASE

//using System.Linq;
//using RecruitMe.Core.Interfaces;
//using RecruitMe.Core.Interfaces.BL;
//using RecruitMe.Core.Interfaces.Models;
//using RecruitMe.Web.Logic.Core.Interfaces;
//using RecruitMe.Web.Logic.Database;
//using RecruitMe.Web.Logic.Services.Skill.Models;
//using RecruitMe.Web.Logic.Services.UserService.Models;

//namespace RecruitMe.Web.Logic.Services.UserService.BusinessRules
//{
//    public class NoDuplicateSkillNames : IInsertRule<SkillViewModel>
//    {
//        public bool Run(SkillViewModel model, IUnitOfWork unitOfWork, Response<SkillViewModel> result, ICoreUser user)
//        {
//            var otherSkills = unitOfWork.With<Database.Skill>().Any(c => c.SkillName == model.SkillName);
//            if (otherSkills)
//            {
//                result.Messages.Add(new Message
//                {
//                    MessageText = "Skill with this name already exists",
//                    SeverityLevel = "Danger"
//                });
//            }
//            return !otherSkills;
//        }
//    };
//}
