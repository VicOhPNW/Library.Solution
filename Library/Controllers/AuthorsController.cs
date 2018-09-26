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
        return View();
      }
    }
}
