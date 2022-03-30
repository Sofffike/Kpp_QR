using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MessagingToolkit.QRCode.Codec;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Upload_File_MVC_Core.Models;

namespace Upload_File_MVC_Core.Controllers
{

    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode("Ntrcn.", QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);

        //    var bitmapBytes = BitmapToBytes(qrCodeImage); //Convert bitmap into a byte array
        //    return File(bitmapBytes, "image/jpeg"); //Return as file result
        //}

        //// This method is for converting bitmap into a byte array
        //private static byte[] BitmapToBytes(Bitmap img)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}






        FilesContext db;
        IWebHostEnvironment Environment;
        public HomeController(FilesContext context, IWebHostEnvironment _environment)
        {
            db = context;
            Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
     
        public IActionResult Index(List<IFormFile> postedFiles, string FileName, Persons persons, string qrcode)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            // путь к папке Uploads
            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> Сотрудник создан, фото сохранено. <br />", fileName);
                }

                if (uploadedFiles != null)
                {
                    Models.File file = new Models.File { FileName = fileName, Path = path + '\\' + fileName };
                    db.File.Add(file);
                    persons.FileName = fileName;

                    db.Persons.Add(persons);

                    db.File.Add(file);
                    db.SaveChanges();
                }

            

            }


            //генерация qr
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();


        }

    }
}



