using EcommerceApplication.Data;
using EcommerceApplication.Data.Services;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAll();
            return View(allCinemas);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("PicUpload,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            //Upload Picture
            if (cinema.PicUpload?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "actors\\");
                string fileName = Guid.NewGuid().ToString() + "_" + cinema.PicUpload.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    cinema.PicUpload.CopyTo(stream);
                }
                cinema.Logo = "/images/actors/" + fileName;
            }
            await _service.Add(cinema);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetById(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetById(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,PicUpload,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            //Upload Picture
            if (cinema.PicUpload?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "actors\\");
                string fileName = Guid.NewGuid().ToString() + "_" + cinema.PicUpload.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    cinema.PicUpload.CopyTo(stream);
                }
                cinema.Logo = "/images/actors/" + fileName;
            }
            await _service.UpdateAsync(id,cinema);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetById(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            //var cinemaDetails = await _service.GetById(id);
            //if (cinemaDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
