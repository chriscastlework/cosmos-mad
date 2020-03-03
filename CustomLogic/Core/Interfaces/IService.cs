using System.Threading.Tasks;
using CustomLogic.Core.Interfaces.Models;

namespace  CustomLogic.Core.Interfaces
{
    /// <summary>
    /// T is view model 
    /// </summary>
    /// <typeparam name="T">View Model</typeparam>
    public interface IService<T>
    {
        Task<Response<T>> Insert(T model, ICoreUser user);

        Task<Response<T>> Update(T model, ICoreUser user);

        Task<Response<T>> Delete(T model, ICoreUser user);

        Task<Response<T>> View(T model, ICoreUser user);

        Task<NgTable<T>> List(NgTableParams ngTableParams, ICoreUser user);
    }
}

