using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandBoxApp.Application.Contracts;
using SanBoxApp.Persistence;
using Microsoft.Extensions.Configuration;

namespace SandBoxApp.Persistence.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private SandBoxAppDbContext dbContext;

        private bool isCompleated = false;
        private bool _disposedValue;

        public IDbContextTransaction dbContextTransaction = null;

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            var options = new DbContextOptionsBuilder<SandBoxAppDbContext>().UseSqlServer(connectionString).Options;
            this.dbContext = new SandBoxAppDbContext(options);
        }

        public IUnitOfWork UseTransaction()
        {
            this.dbContextTransaction = dbContext.Database.BeginTransaction();
            return this;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)))
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }
            IGenericRepository<T> repository = new GenericRepository<T>(dbContext);
            repositories.Add(typeof(T), repository);

            return repository;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Compleate()
        {
            isCompleated = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    SaveChanges();

                    if (!isCompleated && dbContextTransaction is not null)
                    {
                        dbContextTransaction.Rollback();
                    }
                    else if (isCompleated && dbContextTransaction is not null)
                    {
                        dbContextTransaction.Commit();
                    }
                    dbContextTransaction = null;
                }
                _disposedValue = true;
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~UnitOfWork() 
        { 
            Dispose(false); 
        }
    }
}
