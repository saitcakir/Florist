using Cicekci.Models;
using CicekciDL.Abstract;
using CicekciEL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cicekci.Controllers
{
    public class FlowerController : Controller
    {
        private readonly IFlowerDal _flowerDal;
        private readonly IFlowerDisplayDal _flowerDisplayDal;

        public FlowerController(IFlowerDal flowerDal, IFlowerDisplayDal flowerDisplayDal)
        {
            _flowerDal = flowerDal;
            _flowerDisplayDal = flowerDisplayDal;
        }

        public ActionResult FlowerGallery()
        {

            var result = _flowerDal.GetList();
            return View(result);
        }
        public ActionResult FlowerDetail(int id)
        {
            var flower = _flowerDal.GetByID(id);
            FlowerDisplay model = new FlowerDisplay()
            {
                DisplayDate = DateTime.Now,
                FlowerId = id
            };
            ViewBag.totalDisplay = _flowerDal.GetTotalDisplayCountById(id);
           
            return View(flower);
        }

        public ActionResult FlowerAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FlowerAdd(FlowerViewModel model)
        {

            #region FlowerPicture
            if (model.FlowerPictureUpload != null && model.FlowerPictureUpload.ContentType.Contains("" +
                "image") && model.FlowerPictureUpload.ContentLength > 0)
            {


                string fileName = $"{model.Name}-{Guid.NewGuid().ToString().Replace("-", "")}";

                string uzanti = Path.GetExtension(model.FlowerPictureUpload.FileName);
                string directoryPath =
                    Server.MapPath($"~/FlowerPictures");
                string filePath = Server.MapPath($"~/FlowerPictures/{fileName}{uzanti}");

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                model.FlowerPictureUpload.SaveAs(filePath);
                model.FlowerPicture = $"/FlowerPictures/{fileName}{uzanti}";
            }
            else
            {
                ModelState.AddModelError("", $"Lütfen doğru formatta veya yeterli boyutta resim seçiniz!");
                return View(model);
            }
            #endregion

            Flower flower = new Flower()
            {
                CreatedDate = DateTime.Now,
                Name = model.Name,
                Description = model.Description,
                FlowerPicture = model.FlowerPicture
            };
            _flowerDal.Insert(flower);

            return RedirectToAction("FlowerGallery", "Flower");
        }


        public ActionResult FlowerUpdate(int id)
        {
            Flower model = _flowerDal.GetByID(id);
            FlowerViewModel flower = new FlowerViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                FlowerPicture = model.FlowerPicture
            };

            return View("FlowerAdd", flower);
        }

        [HttpPost]
        public ActionResult FlowerUpdate(FlowerViewModel model)
        {
            #region FlowerPicture
            if (model.FlowerPictureUpload != null && model.FlowerPictureUpload.ContentType.Contains("" +
                "image") && model.FlowerPictureUpload.ContentLength > 0)
            {


                string fileName = $"{model.Name}-{Guid.NewGuid().ToString().Replace("-", "")}";

                string uzanti = Path.GetExtension(model.FlowerPictureUpload.FileName);
                string directoryPath =
                    Server.MapPath($"~/FlowerPictures");
                string filePath = Server.MapPath($"~/FlowerPictures/{fileName}{uzanti}");

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                model.FlowerPictureUpload.SaveAs(filePath);
                model.FlowerPicture = $"/FlowerPictures/{fileName}{uzanti}";

            }
            else
            {
                ModelState.AddModelError("", $"Lütfen doğru formatta veya yeterli boyutta resim seçiniz!");
                return View(model);
            }
            #endregion

            Flower flower = new Flower()
            {
                Name = model.Name,
                Description = model.Description,
                FlowerPicture = model.FlowerPicture
            };
            _flowerDal.Update(flower);

            return View();
        }
    }
}