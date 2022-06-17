using CPSDevExerciseWeb.Database;
using CPSDevExerciseWeb.Models;
using CPSDevExerciseWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CPSDevExerciseWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrdersReader _ordersReader;

        public HomeController(ILogger<HomeController> logger, IOrdersRepository ordersRepository, IOrdersReader ordersReader)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _ordersReader = ordersReader;   
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders(string CustomerName)
        {
            ViewBag.CustomerName = string.Empty;
            ViewBag.Orders = new Order[] { };

            if (Request.Method == "POST")
            {
                ViewBag.CustomerName = CustomerName;
                ViewBag.Orders = _ordersRepository.GetCustomerOrders(CustomerName);
            }
            return View();
        }

        public IActionResult ImportOrders(string XMLContent)
        {
            ViewBag.ValidationErrors = new List<string>();
            if (Request.Method == "POST")
            {
                try
                {
                    var importPackage = _ordersReader.ReadOrders(XMLContent);

                    if (importPackage.ValidationErrors.Count > 0)
                    {
                        ViewBag.ValidationErrors = importPackage.ValidationErrors;
                        ViewBag.XMLContent = XMLContent;
                        return View();
                    }

                    foreach (var order in importPackage.Orders)
                    {
                        _ordersRepository.CreateOrder(order);
                    }

                    return Redirect("/Home/Orders");
                }
                catch(Exception ex)
                {
                    ViewBag.ValidationErrors.Add(ex.Message);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}