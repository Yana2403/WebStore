﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class Error404Controller : Controller
    {
        public IActionResult Error404() => View();
    }
}
