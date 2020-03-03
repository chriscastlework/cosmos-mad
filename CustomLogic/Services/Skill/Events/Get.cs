using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Skill.Mappers;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill.Events
{
    public class View : IViewEvent<SkillViewModel, Database.Skill>
    {
        public int CreatedId = 0;

        public Response<SkillViewModel> Run(SkillViewModel model, ref IQueryable<Database.Skill> repository, IUnitOfWork unitOfWork, Response<SkillViewModel> result, ICoreUser Skill)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.Id == model.Id);

            if (itemToUpdate != null)
            {
                var newCustomResult = SkillMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing Skills");
            }

            return result;
        }

    }
}
