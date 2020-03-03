using CustomLogic.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomLogic.Database
{
    public partial class DatabaseMigratorContext : DbContext, IUnitOfWork
    {
        public readonly string _connectionString;

        public DatabaseMigratorContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        ///  You must copy this to the automatically create class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(_connectionString);
        //    }
        //}

        public DbSet<T> With<T>() where T : class
        {
            return this.Set<T>();
        }
    }
}
