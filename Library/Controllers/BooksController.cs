using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
      [HttpGet("/books")]
      public ActionResult Index()
      {
        List<Book> allBooks = Book.GetAll();
        return View(allBooks);
      }

      [HttpGet("/books/new")]
      public ActionResult CreateForm()
      {
        return View();
      }

      [HttpPost("/books")]
      public ActionResult Create()
      {
        Book newBook = new Book(Request.Form["title"],Request.Form["author"], (Convert.ToInt32(Request.Form["copies"])),Request.Form["description"]);
        newBook.Save();
        return RedirectToAction("Index");
      }

      // [HttpGet("/books/{id}")]
      // public ActionResult Details(int id)
      // {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Book selectedBook = Book.Find(id);
      //   List<Author> bookAuthors = selectedBook.GetAuthors();
      //   List<Author> allAuthors = Author.GetAll();
      //   model.Add("selectedBook", selectedBook);
      //   model.Add("bookAuthors", bookAuthors);
      //   model.Add("allAuthors", allAuthors);
      //   return View(model);
      // }
    }
}
