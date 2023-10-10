using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.DTOs
{
    public class PaginationParameters<T>
    {
        public string Filter;
        public string FilterBy;
        public string SortBy;
        public bool SortByDesceding;
        public int PageNumber;
        public int PageSize;
        public Expression<Func<T, bool>> Filterpredicate = x => true;
    }
}
