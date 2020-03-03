using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.JobSkill.Mappers;
using CustomLogic.Services.JobSkill.Models;

namespace CustomLogic.Services.JobSkill.Events
{
    public class QueryLogic : IViewListEvent<JobSkillViewModel, Database.JobSkill>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.JobSkill> repository, NgTable<JobSkillViewModel> result, ICoreUser JobSkill, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<JobSkillViewModel>();

            var query = JobSkillMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

