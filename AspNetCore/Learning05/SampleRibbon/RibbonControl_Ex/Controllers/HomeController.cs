﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace RibbonControl_Ex
{
    public class HomeController2 : Controller
    {
        private readonly ILogger<HomeController2> _logger;

        public HomeController2(ILogger<HomeController2> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
