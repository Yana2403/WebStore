using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Login() => View();
        public IActionResult Checkout() => View();
        public IActionResult Cart() => View();

    }
}
