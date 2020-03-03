using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateSkill.Mappers;
using CustomLogic.Services.CandidateSkill.Models;

namespace CustomLogic.Services.CandidateSkill.Events
{
    public class QueryLogic : IViewListEvent<CandidateSkillViewModel, Database.CandidateSkill>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.CandidateSkill> repository, NgTable<CandidateSkillViewModel> result, ICoreUser CandidateSkill, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<CandidateSkillViewModel>();

            var query = CandidateSkillMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

