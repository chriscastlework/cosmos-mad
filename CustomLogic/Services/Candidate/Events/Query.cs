using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Candidate.Mappers;
using CustomLogic.Services.Candidate.Models;

namespace CustomLogic.Services.Candidate.Events
{
    public class QueryLogic : IViewListEvent<CandidateViewModel, Database.Candidate>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.Candidate> repository, NgTable<CandidateViewModel> result, ICoreUser Candidate, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<CandidateViewModel>();

            var query = CandidateMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

