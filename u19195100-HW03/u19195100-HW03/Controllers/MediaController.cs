using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using u19195100_HW03.Models;

namespace u19195100_HW03.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file, FormCollection collection)
        {
            //Recive option from radio button using form collection
            string Input = Convert.ToString(collection["Option"]);

            //check the option
            if (Input == "Document")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Files/Documents"), file.FileName));
            }
            else if (Input == "Image")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Files/Images"), file.FileName));
            }
            else
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Files/Videos"), file.FileName));
            }
            return RedirectToAction("Home");
        }

        public ActionResult Files()
        {
            List<FileModel> mydatafiles = new List<FileModel>();

            string[] myDocuments = Directory.GetFiles(Server.MapPath("~/Content/Files/Documents"));
            

            foreach (var Document in myDocuments)
            {
                FileModel FoundDocument = new FileModel();
                FoundDocument.FileName = Path.GetFileName(Document);
                FoundDocument.FileType = "doc";
                mydatafiles.Add(FoundDocument);
            }
           

            return View(mydatafiles);
        }

        public ActionResult Images()
        {
            List<FileModel> myPhotos = new List<FileModel>();
            string[] Photolocations = Directory.GetFiles(Server.MapPath("~/Content/Files/Images"));
            foreach (var file in Photolocations)
            {
                FileModel FoundPhoto = new FileModel();
                FoundPhoto.FileName = Path.GetFileName(file);
                FoundPhoto.FileType = "img";
                myPhotos.Add(FoundPhoto);
            }
            return View(myPhotos);
        }

        public ActionResult Videos()
        {
            List<FileModel> myGraphical = new List<FileModel>();
            string[] Graphiclocation = Directory.GetFiles(Server.MapPath("~/Content/Files/Videos"));
            foreach (var file in Graphiclocation)
            {
                FileModel FoundGraphic = new FileModel();
                FoundGraphic.FileName = Path.GetFileName(file);
                FoundGraphic.FileType = "vid";
                myGraphical.Add(FoundGraphic);
            }
            return View(myGraphical);
        }

        public ActionResult AboutMe()
        {

            return View();
        }

        public FileResult DownloadFile(string fileName, string fileType)
        {
            byte[] storagecapacity = null;
            if (fileType == "doc")
            {
                storagecapacity = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Files/Documents/") + fileName);
            }
            else if (fileType == "vid")
            {
                storagecapacity = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Files/Videos/") + fileName);
            }
            else
            {
                storagecapacity = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Files/Images/") + fileName);
            }
            return File(storagecapacity, "application/octet-stream", fileName);
        }

        public ActionResult DeleteFile(string fileName, string fileType)
        {
            string Foundfilelocation = null;

            if (fileType == "doc")
            {
                Foundfilelocation = Server.MapPath("~/Content/Files/Documents/") + fileName;
            }
            else if (fileType == "vid")
            {
                Foundfilelocation = Server.MapPath("~/Content/Files/Videos/") + fileName;
            }
            else
            {
                Foundfilelocation = Server.MapPath("~/Content/Files/Images/") + fileName;
            }

            System.IO.File.Delete(Foundfilelocation);
            return RedirectToAction("Home");
        }
    }
}