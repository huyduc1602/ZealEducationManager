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
        bool checkDuplicate(Expression<Func<T, bool>> predicate);
        bool add(T model);
        bool edit(T model);
        T findById(object id);
        bool remove(object id);
        bool remove(T item);
    }
}
