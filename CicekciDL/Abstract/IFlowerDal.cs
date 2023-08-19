using CicekciEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciDL.Abstract
{
    public interface IFlowerDal : IGenericDal<Flower>
    {
    int GetTotalDisplayCountById(int id);
    }
}
