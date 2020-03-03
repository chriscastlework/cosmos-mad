using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Employer.Mappers;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer.Events
{
    public class QueryLogic : IViewListEvent<EmployerViewModel, Database.Employer>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.Employer> repository, NgTable<EmployerViewModel> result, ICoreUser Employer, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<EmployerViewModel>();

            var query = EmployerMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

