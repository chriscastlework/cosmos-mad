using Microsoft.EntityFrameworkCore;

namespace  CustomLogic.Core.Interfaces
{
    public interface IUnitOfWork
    {
        DbSet<T> With<T>() where T : class;
        int SaveChanges();
    }
}

