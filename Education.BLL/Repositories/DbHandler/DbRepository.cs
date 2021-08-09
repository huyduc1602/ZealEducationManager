using Education.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.BLL
{
    public class DbRepository<T> : IRepository<T> where T : class, new()
    {
        private EducationManageDbContext context;

        private DbSet<T> tbl; 

        public DbRepository()
        {
            context = new EducationManageDbContext();
            tbl = context.Set<T>();
        }
        public bool add(T model)
        {
            try
            {
                tbl.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool checkDuplicate(Expression<Func<T, bool>> predicate)
        {
            return tbl.AsNoTracking().Any(predicate);
        }

        public bool edit(T model)
        {
            try
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T findById(object id)
        {
            try
            {
                return tbl.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<T> Get()
        {
            return tbl.AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return tbl.Where(predicate).AsEnumerable();
        }

        public bool remove(object id)
        {
            try
            {
                var entity = findById(id);
                tbl.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool remove(T item)
        {
            try
            {
                tbl.Remove(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
