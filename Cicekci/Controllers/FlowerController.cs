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

            FlowerDisplay flowerDisplay = new FlowerDisplay()
            {

                DisplayDate = DateTime.Now,
                FlowerId = id
            };
            _flowerDisplayDal.Insert(flowerDisplay);
            ViewBag.totalDisplay = _flowerDal.GetTotalDisplayCountById(id);

            return View(flower);
        }

        [Authorize]
        public ActionResult FlowerAdd()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult FlowerDelete(int id)
        {
            JqueryModel jqueryModel = new JqueryModel();
            var result = _flowerDal.Delete(id);
            if (result)
            {
                jqueryModel = new JqueryModel()
                {
                    Message = "ok",
                    Status = true
                };
            }
            else
            {
                jqueryModel = new JqueryModel()
                {
                    Message = "silme işlemi başarısız",
                    Status = false
                };
            }

            return Json(jqueryModel);
        }

        [Authorize]
        public ActionResult FlowerList()
        {
            var result = _flowerDal.GetList();
            return View(result);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public ActionResult FlowerUpdate(FlowerViewModel model)
        {

            var editedFlower = _flowerDal.GetByID(model.Id);
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
            else if (editedFlower.FlowerPicture != null)
            {
                model.FlowerPicture = editedFlower.FlowerPicture;
            }
            else
            {
                ModelState.AddModelError("", $"Lütfen doğru formatta veya yeterli boyutta resim seçiniz!");
                return View("FlowerAdd", model);
            }
            #endregion


            Flower flower = new Flower()
            {
                Id = model.Id,
                CreatedDate = editedFlower.CreatedDate,
                Name = model.Name,
                Description = model.Description,
                FlowerPicture = model.FlowerPicture
            };
            _flowerDal.Update(flower);
            ViewBag.Message = "Update Successfull";
            return View("FlowerAdd", model);
        }
    }
}