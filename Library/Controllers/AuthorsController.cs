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


    }
}
