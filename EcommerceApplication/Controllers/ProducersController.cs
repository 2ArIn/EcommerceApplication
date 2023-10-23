using EcommerceApplication.Data;
using EcommerceApplication.Data.Services;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAll();
            return View(allProducers);
        }
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetById(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,Bio,PicUpload")]Producer producer)
        {
            if (!ModelState.IsValid) 
                return View(producer);

            //Upload Picture
            if (producer.PicUpload?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "actors\\");
                string fileName = Guid.NewGuid().ToString() + "_" + producer.PicUpload.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    producer.PicUpload.CopyTo(stream);
                }
                producer.ProfilePictureURL = "/images/actors/" + fileName;
            }
            await _service.Add(producer);
            return RedirectToAction("Index");
        } 
        public async Task<IActionResult> Edit(int id)
        {
            var producersDetail = await _service.GetById(id);
            if (producersDetail == null) return View("NotFound");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,Bio,PicUpload")]Producer producer)
        {
            if (!ModelState.IsValid) 
                return View(producer);

            //Upload Picture
            if (producer.PicUpload?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "actors\\");
                string fileName = Guid.NewGuid().ToString() + "_" + producer.PicUpload.FileName;
                string path = filePath + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    producer.PicUpload.CopyTo(stream);
                }
                producer.ProfilePictureURL = "/images/actors/" + fileName;
            }
            if(producer.Id == id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction("Index");
            }
            return View(producer);
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producersDetail = await _service.GetById(id);
            if (producersDetail == null) return View("NotFound");
            return View(producersDetail);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producersDetail = await _service.GetById(id);
            if (producersDetail == null) return View("NotFound");
            await _service.Delete(id);
            return RedirectToAction(nameof(Index)); 

        }
    }
}
