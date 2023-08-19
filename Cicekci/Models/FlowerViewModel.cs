using CicekciEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicekci.Models
{
    public class FlowerViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FlowerPicture { get; set; }

        public HttpPostedFileBase FlowerPictureUpload { get; set; }

        public virtual ICollection<FlowerDisplay> FlowerDisplay { get; set; }
    }
}