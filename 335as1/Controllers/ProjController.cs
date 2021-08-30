using _335as1.Data;
using _335as1.Dtos;
using _335as1.Helper;
using _335as1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace _335as1.Controllers {
    [Route("api")]
    [ApiController]
    public class ProjController : Controller {

        private readonly IAPIRepo _repository;

        public ProjController(IAPIRepo repository) {
            _repository = repository;
        }


        [HttpGet("GetLogo")]
        public ActionResult GetLogo() {

            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "Images/StaffPhotos");
            string fileName1 = Path.Combine(imgDir, "Logo" + ".png");
            string fileName2 = Path.Combine(imgDir, "Logo" + ".jpg");
            string fileName3 = Path.Combine(imgDir, "Logo" + ".gif");
            string respHeader = "";
            string fileName = "";
            if(System.IO.File.Exists(fileName1)) {
                respHeader = "image/png";
                fileName = fileName1;
            }
            else if(System.IO.File.Exists(fileName2)) {
                respHeader = "image/jpeg";
                fileName = fileName2;
            }
            else if(System.IO.File.Exists(fileName3)) {
                respHeader = "image/gif";
                fileName = fileName3;
            }
            else
                return NotFound();
            return PhysicalFile(fileName, respHeader);
        }

        [HttpGet("GetVersion")]
        public ActionResult GetVersion() {
            string s = "v1";
            return Ok(s);
        }

        [HttpGet("GetAllStaff")]
        public ActionResult<IEnumerable<StaffOutDto>> GetAllStaff() {
            IEnumerable<Staff> staffs = _repository.GetAllStaff();
            IEnumerable<StaffOutDto> s = staffs.Select(e => new StaffOutDto { 
                Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, Title = e.Title, Email = e.Email, Tel = e.Tel, Url = e.Url, Research = e.Research});
            return Ok(s);
        }

        [HttpGet("GetStaffPhoto/{name}")]
        public ActionResult GetStaffPhoto(string name) {

            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "Images/StaffPhotos");
            string fileName1 = Path.Combine(imgDir, name + ".png");
            string fileName2 = Path.Combine(imgDir, name + ".jpg");
            string fileName3 = Path.Combine(imgDir, name + ".gif");
            string filename4 = Path.Combine(imgDir, "default" + ".png");
            string respHeader = "";
            string fileName = "";
            if(System.IO.File.Exists(fileName1)) {
                respHeader = "image/png";
                fileName = fileName1;
            }
            else if(System.IO.File.Exists(fileName2)) {
                respHeader = "image/jpeg";
                fileName = fileName2;
            }
            else if(System.IO.File.Exists(fileName3)) {
                respHeader = "image/gif";
                fileName = fileName3;
            }
            else {
                respHeader = "image/png";
                fileName = filename4;
            }
            return PhysicalFile(fileName, respHeader);
        }

        [HttpGet("GetCard/{id}")]
        public ActionResult GetCard(int id) {

            Staff staff = _repository.GetStaffByID(id);
            string path = Directory.GetCurrentDirectory();
            string fileName1 = Path.Combine(path, "Images/StaffPhotos/" + id + ".png");
            string fileName2 = Path.Combine(path, "Images/StaffPhotos/" + id + ".jpg");
            string fileName3 = Path.Combine(path, "Images/StaffPhotos/" + id + ".gif");
            string fileName4 = Path.Combine(path, "Images/StaffPhotos/default" + ".png");
            string fileName = "";
            if(System.IO.File.Exists(fileName1)) {
                fileName = fileName1;
            }
            else if(System.IO.File.Exists(fileName2)) {
                fileName = fileName2;
            }
            else if(System.IO.File.Exists(fileName3)) {
                fileName = fileName3;
            }
            else {
                fileName = "";
            }
            string photoString, photoType;
            string logoString, logoType;
            ImageFormat imageFormat;
            ImageFormat logoFormat;
            if(System.IO.File.Exists(fileName)) {
                Image image = Image.FromFile(fileName);
                imageFormat = image.RawFormat;
                image = ImageHelper.Resize(image, new Size(200, 200), out photoType);
                photoString = ImageHelper.ImageToString(image, imageFormat);
            }
            else {
                photoString = "";
                photoType = "JPEG";
            }

            if(System.IO.File.Exists(fileName4)) {
                Image logo = Image.FromFile(fileName4);
                logoFormat = logo.RawFormat;
                logo = ImageHelper.Resize(logo, new Size(100, 100), out logoType);
                logoString = ImageHelper.ImageToString(logo, logoFormat);
            }
            else
                return NotFound();



            CardOutDto cardOut = new CardOutDto();
            if(staff == null) {
                cardOut.FirstName = "";
                cardOut.LastName = "";
                cardOut.Title = "";
                cardOut.Name = "";
                cardOut.Uid = null;
                cardOut.Org = "";
                cardOut.Email = "";
                cardOut.Tel = "";
                cardOut.Url = "";
                cardOut.Photo = photoString;
                cardOut.PhotoType = photoType;
                cardOut.Logo = logoString;
                cardOut.LogoType = logoType;
                cardOut.Categories = Helper.ResearchFilter.Filter("");
                Response.Headers.Add("Content-Type", "text/vcard");
            }
            else {
                cardOut.FirstName = staff.FirstName;
                cardOut.LastName = staff.LastName;
                cardOut.Title = staff.Title;
                cardOut.Name = staff.Title + " " + staff.FirstName + " " + staff.LastName;
                cardOut.Uid = staff.Id;
                cardOut.Org = "Southern Hemisphere Institue of Technology";
                cardOut.Email = staff.Email;
                cardOut.Tel = staff.Tel;
                cardOut.Url = staff.Url;
                cardOut.Photo = photoString;
                cardOut.PhotoType = photoType;
                cardOut.Logo = logoString;
                cardOut.LogoType = logoType;
                cardOut.Categories = Helper.ResearchFilter.Filter(staff.Research);
                Response.Headers.Add("Content-Type", "text/vcard");
            }
            return Ok(cardOut);
        }

        [HttpGet("GetItems/{name}")]
        [HttpGet("GetItems")]
        public ActionResult<IEnumerable<ProductOutDto>> GetItems(string name) {
            if(String.IsNullOrWhiteSpace(name)) {
                IEnumerable<Product> products = _repository.GetItems();
                IEnumerable<ProductOutDto> p = products.Select(e => new ProductOutDto {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Price = e.Price
                });
                return Ok(p);
            }
            else {
                IEnumerable<Product> products = _repository.GetItems(name);
                IEnumerable<ProductOutDto> p = products.Select(e => new ProductOutDto {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Price = e.Price
                });
                return Ok(p);
            }
        }

        [HttpGet("GetItemPhoto/{id}")]
        public ActionResult GetItemPhoto(int id) {

            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "Images/ItemsImages");
            string fileName1 = Path.Combine(imgDir, id + ".png");
            string fileName2 = Path.Combine(imgDir, id + ".jpg");
            string fileName3 = Path.Combine(imgDir, id + ".gif");
            string filename4 = Path.Combine(imgDir, "default" + ".png");
            string respHeader = "";
            string fileName = "";
            if(System.IO.File.Exists(fileName1)) {
                respHeader = "image/png";
                fileName = fileName1;
            }
            else if(System.IO.File.Exists(fileName2)) {
                respHeader = "image/jpeg";
                fileName = fileName2;
            }
            else if(System.IO.File.Exists(fileName3)) {
                respHeader = "image/gif";
                fileName = fileName3;
            }
            else {
                respHeader = "image/png";
                fileName = filename4;
            }
            return PhysicalFile(fileName, respHeader);
        }

        [HttpPost("WriteComment")]
        public ActionResult WriteComment(CommentInputDto comment) {
            Comments c = new Comments { Comment = comment.Comment, Name = comment.Name, Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(), Time = DateTime.Now };
            Comments addedComment = _repository.WriteComment(c);
            //CommentOutDto co = new CommentOutDto { Id = addedComment.Id, Comment = addedComment.Comment, Name = addedComment.Name, Ip = addedComment.Ip, Time = addedComment.Time };
            //return CreatedAtAction(nameof(WriteComment), new { id = co.Id }, co);
            return Ok(addedComment.Comment);
        }

        [HttpGet("GetComments")]
        public ActionResult GetComments() {
            IEnumerable<Comments> comments = _repository.GetComments();
            comments = comments.Reverse();
            String last5Out = "<html><head><title></title></head><body>\n";
            foreach(Comments com in comments) {
                String s = string.Format("<p>{0} &mdash; {1}</p>\n", com.Comment, com.Name);
                last5Out = last5Out + s;
            }
            last5Out = last5Out + "</body></html>";



            ContentResult c = new ContentResult {
                Content = last5Out,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
            };
            return c;
        }

        public IActionResult Index() {
            return View();
        }
    } 
}
