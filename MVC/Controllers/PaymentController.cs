using BLLServices.Payment;
using BLLServices.Payment.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PaymentController(PayPalClient client) : Controller
    {
        public IActionResult Index()
        {
            // Initialize the PayPal client
            ViewBag.ClientId = client.ClientId;
            return View();
        }

        public IActionResult Buy(string value, string currency, string reference)
        {
            // Create an order using the PayPal client
            CreateOrderResponse order = client.CreateOrder(value, currency, reference).Result;
            return Redirect(order.Links.FirstOrDefault(x => x.Rel == "approve")?.Href ?? "/");
        }
    }
}
