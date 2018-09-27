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

    [HttpGet("/books/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Book selectedBook = Book.Find(id);
      List<Author> authorBooks = selectedBook.GetAuthors();
      List<Author> allAuthors = Author.GetAll();
      model.Add("selectedBook", selectedBook);
      model.Add("authorBooks", authorBooks);
      model.Add("allAuthors", allAuthors);
      return View(model);
    }


    [HttpPost("/books/{bookId}/authors/new")]
    public ActionResult AddAuthor(int bookId)
    {
      Book book = Book.Find(bookId);
      Author author = Author.Find(Int32.Parse(Request.Form["author-id"]));
      book.AddAuthor(author);
      return RedirectToAction("Index");
    }

    [HttpGet("/books/{bookId}/update")]
    public ActionResult UpdateForm(int bookId)
    {
      Book thisBook = Book.Find(bookId);
      return View("UpdateForm", thisBook);
    }

    [HttpPost("/books/{bookId}/update")]
    public ActionResult Update(int bookId)
    {
      Book thisBook = Book.Find(bookId);
      thisBook.Update(Request.Form["newTitle"], Request.Form["newAuthor"], (Convert.ToInt32(Request.Form["newCopies"])), Request.Form["newDescription"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/books/{bookId}/delete")]
    public ActionResult DeleteOne(int bookId)
    {
      Book thisBook = Book.Find(bookId);
      thisBook.Delete();
      return RedirectToAction("Index");
    }

    [HttpPost("/books/search")]
    public ActionResult Search()
    {

      List<Book> allBooksB

      Book thisBook = Book.Find(bookId);
      thisBook.Update(Request.Form["newTitle"], Request.Form["newAuthor"], (Convert.ToInt32(Request.Form["newCopies"])), Request.Form["newDescription"]);
      return RedirectToAction("Index");
    }

  }
}
