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
        public bool Add(T model)
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

        public bool CheckDuplicate(Expression<Func<T, bool>> predicate)
        {
            return tbl.AsNoTracking().Any(predicate);
        }

        public bool Edit(T model)
        {
            try
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception E)
            {
                string e = E.Message;
                return false;
            }
        }

        public T FindById(object id)
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

        public bool Remove(object id)
        {
            try
            {
                var entity = FindById(id);
                tbl.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(T item)
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
