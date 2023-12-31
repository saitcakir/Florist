﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciEL.Entities
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FlowerPicture { get; set; }

        public virtual ICollection<FlowerDisplay> FlowerDisplay { get; set; }

    }
}
