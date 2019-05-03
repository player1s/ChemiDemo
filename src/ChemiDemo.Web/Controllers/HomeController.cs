namespace ChemiDemo.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController 
        : Controller
    {
        public IActionResult Index()
        {
            System.Console.WriteLine("homecont got called");
           return View();
        }
    }
}