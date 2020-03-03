using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.CandidateResponse.Mappers;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse.Events
{
    public class QueryLogic : IViewListEvent<CandidateResponseViewModel, Database.CandidateResponse>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.CandidateResponse> repository, NgTable<CandidateResponseViewModel> result, ICoreUser CandidateResponse, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<CandidateResponseViewModel>();

            var query = CandidateResponseMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

