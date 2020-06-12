using System;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Throw(string id) => throw new ApplicationException(id ?? "Error!"); //генерация исключений
    }

}
