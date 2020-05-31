using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VisioForge.Shared.MediaFoundation.OPM;
using VisioForge.Tools.TagLib.Asf;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Throw(string id) => throw new ApplicationException(id ?? "Error!"); //генерация исключений
    }

}
