using BookStoreWeb.Models.Domain;
using BookStoreWeb.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        private readonly IGenreService genreService;
        public BookController(IBookService bookservice, IAuthorService authorService, IPublisherService publisherService, IGenreService genreService)
        {
            this.bookService = bookservice;
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.genreService = genreService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem {Text = a.AuthorName, Value = a.Id.ToString()}).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected=a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Add(model);
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
            var model = bookService.FindById(id);
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Update(model);
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

            var result = bookService.Delete(id);
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

            var data = bookService.GetAll();
            return View(data);
        }
    }
}
