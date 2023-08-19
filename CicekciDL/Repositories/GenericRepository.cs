using CicekciDL.Abstract;
using CicekciDL.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciDL.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
      public  Context context = new Context();
        public bool Delete(int id)
        {
            try
            {
                T t = context.Set<T>().Find(id);
                context.Set<T>().Remove(t);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public T GetByID(int id)
        {

            return context.Set<T>().Find(id);

        }

        public List<T> GetList()
        {
            return context.Set<T>().ToList();
        }

        public bool Insert(T t)
        {
            context.Set<T>().Add(t);
            return (context.SaveChanges() > 0) ? true : false;

        }

        public bool Update(T t)
        {
            context.Set<T>().AddOrUpdate(t);
            return (context.SaveChanges() > 0) ? true : false;
        }
    }
}
