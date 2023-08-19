using CicekciDL.Abstract;
using CicekciDL.Repositories;
using CicekciEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciDL.EntityFramework
{
    public class EfFlowerDisplayDal:GenericRepository<FlowerDisplay>,IFlowerDisplayDal
    {

    }
}
