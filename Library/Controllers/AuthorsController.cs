using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }

    [HttpGet("/authors/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/authors")]
    public ActionResult Create()
    {
      Author newAuthor = new Author(Request.Form["name"]);
      newAuthor.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/authors/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Author selectedAuthor = Author.Find(id);
      List<Book> listBooks = selectedAuthor.GetBooks();
      model.Add("selectedAuthor", selectedAuthor);
      model.Add("listBooks", listBooks);
      return View(model);

    }


  }
}
