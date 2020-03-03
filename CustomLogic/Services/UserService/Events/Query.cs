using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Database;
using CustomLogic.Services.UserService.Mappers;
using CustomLogic.Services.UserService.Models;

namespace CustomLogic.Services.UserService.Events
{
    public class QueryLogic : IViewListEvent<UserViewModel, User>
    {

            public bool Run(NgTableParams model, ref IQueryable<User> repository, NgTable<UserViewModel> result, ICoreUser user, IUnitOfWork db)
            {
            var ngTransformer = new QueryToNgTable<UserViewModel>();

            var query = UsersMapper.MapDbModelQueryToViewModelQuery(repository);

            ngTransformer.ToNgTableDataSet(model, query, result);
            return true;
        }
    }
}

