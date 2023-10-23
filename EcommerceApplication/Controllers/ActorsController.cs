using EcommerceApplication.Data;
using EcommerceApplication.Data.Services;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

namespace EcommerceApplication.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ActorsController(IActorService service, IWebHostEnvironment hostEnvironment)
        {
            _service = service;
            _hostEnvironment = hostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAll();
            return View(actors);
        }
        public async  Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,Bio,PicUpload")]Actor actor)
        {
           

            if (!ModelState.IsValid) 
            {
                return View(actor);
            }

            //Upload Picture
            if (actor.PicUpload?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "actors\\");
                string fileName = Guid.NewGuid().ToString() + "_" + actor.PicUpload.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path , FileMode.Create))
                {
                    actor.PicUpload.CopyTo(stream);
                }
                actor.ProfilePictureURL = "/images/actors/"+fileName;
            }



            //string wwwRootPath = _hostEnvironment.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(actor.PicUpload.FileName);
            //string extension = Path.GetExtension(actor.PicUpload.FileName);
             
            //string filePath= "/images/actors/"+fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //actor.ProfilePictureURL = filePath;
            //string path = Path.Combine(wwwRootPath,filePath);
            //using (var fileStream = new FileStream(path, FileMode.Create))
            //{
            //    await actor.PicUpload.CopyToAsync(fileStream);
            //}


            await _service.Add(actor);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,Bio,ProfilePictureURL,PicUpload")] Actor actor)
        {

            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            //Upload Picture
            if (actor.PicUpload?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "actors\\");
                string fileName = Guid.NewGuid().ToString() + "_" + actor.PicUpload.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    actor.PicUpload.CopyTo(stream);
                }
                actor.ProfilePictureURL = "/images/actors/" + fileName;
            }

            await _service.UpdateAsync(id,actor);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}