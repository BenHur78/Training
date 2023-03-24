using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstProject.Models;

namespace FirstProject.Controllers;

public class HomeController : Controller
{
    public string Index() {
        return "Hello World";
    }
}
