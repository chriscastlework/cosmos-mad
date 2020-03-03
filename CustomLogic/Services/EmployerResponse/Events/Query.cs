using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.EmployerResponse.Mappers;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse.Events
{
    public class QueryLogic : IViewListEvent<EmployerResponseViewModel, Database.EmployerResponse>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.EmployerResponse> repository, NgTable<EmployerResponseViewModel> result, ICoreUser EmployerResponse, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<EmployerResponseViewModel>();

            var query = EmployerResponseMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

