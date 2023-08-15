using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciEL.Entities
{
    public class FlowerDisplay
    {
        public int Id { get; set; }
        public int FlowerId { get; set; }
        public DateTime DisplayDate { get; set; }

        [ForeignKey("FlowerId")]
        public virtual Flower Flower { get; set; }
    }
}
