using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciDL.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        bool Insert(T t);
        bool Delete(int id);
        bool Update(T t);
        T GetByID(int id);
        List<T> GetList();
    }
}
