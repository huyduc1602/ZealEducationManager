using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.BLL
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        bool CheckDuplicate(Expression<Func<T, bool>> predicate);
        bool Add(T model);
        bool Edit(T model);
        T FindById(object id);
        bool Remove(object id);
        bool Remove(T item);
    }
}
