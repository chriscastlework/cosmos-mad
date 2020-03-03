using CustomLogic.Core.Interfaces.Models;

namespace CustomLogic.Core.Interfaces.DL
{

    internal interface IInsertEvent<T>
    {
        bool Run(T model, IUnitOfWork unitOfWork, Response<T> result, ICoreUser user);
    }
}
