
using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Job.Mappers;
using CustomLogic.Services.Job.Models;

namespace CustomLogic.Services.Job.Events
{
    public class QueryLogic : IViewListEvent<JobViewModel, Database.Job>
    {

            public bool Run(NgTableParams model, ref IQueryable<Database.Job> repository, NgTable<JobViewModel> result, ICoreUser Job, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<JobViewModel>();

            var query = JobMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

