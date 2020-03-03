using System.Linq;
using CustomLogic.Core.Interfaces.Models;

namespace CustomLogic.Core.Interfaces.DL
{
    internal interface IDeleteEvent<T, T2>
    {
        bool Run(T model, ref IQueryable<T2> repository, IUnitOfWork unitOfWork, Response<T> result, ICoreUser user);
    }
}
