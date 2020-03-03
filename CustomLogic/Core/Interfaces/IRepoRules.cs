using System.Linq;
using CustomLogic.Core.Interfaces.Models;

namespace CustomLogic.Core.Interfaces
{
    public interface IRepoRule<T, T2> where T2 : class
    {
        bool Run(T model, ref IQueryable<T2> repository, IUnitOfWork unitOfWork, Response<T> result, ICoreUser user);
    }
}
