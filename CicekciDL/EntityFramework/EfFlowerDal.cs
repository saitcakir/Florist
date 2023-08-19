using CicekciDL.Abstract;
using CicekciDL.Repositories;
using CicekciEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CicekciDL.EntityFramework
{
    public class EfFlowerDal : GenericRepository<Flower>, IFlowerDal
    {

        int IFlowerDal.GetTotalDisplayCountById(int id)
        {
            return context.FlowerDisplays.Include(x => x.Flower).Where(x => x.FlowerId == id).ToList().Count();
        }
    }
}
