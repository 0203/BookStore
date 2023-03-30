using BookStoreWeb.Models.Domain;
using BookStoreWeb.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService service;
        public AuthorController(IAuthorService service)
        {
                this.service = service;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Failed. Error while inserting data.";
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var record = service.FindById(id);   
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Failed. Error while updating data.";
            return View(model);
        }

        public IActionResult Delete(int id)
        {

            var result = service.Delete(id);
            if (result)
            {
                TempData["msg"] = "Deleted Successfully";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Failed. Error while deleting data.";
            return View("GetAll");
        }

        public IActionResult GetAll(int id)
        {

            var data = service.GetAll();
            return View(data);
        }
    }
}
