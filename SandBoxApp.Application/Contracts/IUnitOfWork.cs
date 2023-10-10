using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.Contracts
{
    public interface IUnitOfWork
    {
        void Compleate();
        void Dispose();
        IGenericRepository<T> Repository<T>() where T : class;
        void SaveChanges();
        IUnitOfWork UseTransaction();
    }
}
