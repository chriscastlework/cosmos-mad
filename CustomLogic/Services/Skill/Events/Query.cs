using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Skill.Mappers;
using CustomLogic.Services.Skill.Models;

namespace CustomLogic.Services.Skill.Events
{
    public class QueryLogic : IViewListEvent<SkillViewModel, Database.Skill>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.Skill> repository, NgTable<SkillViewModel> result, ICoreUser Skill, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<SkillViewModel>();

            var query = SkillMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

