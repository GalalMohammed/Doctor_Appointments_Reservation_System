using BLLServices.Payment;
using BLLServices.Payment.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace MVC.Controllers
{
    public class PaymentController(PayPalClient client) : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ClientId = client.ClientId;
            ViewBag.amount = 100;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrder([FromBody]JsonObject data)
        {
            // Validate the incoming JSON object
            if (!data.ContainsKey("amount") || data["amount"] == null || data["amount"]!.ToString() == null)
            {
                return Json(new { error = "Invalid amount" });
            }
            // Set the amount and currency from the JSON object
            string amount = data["amount"]!.ToString();
            // Create an order using the PayPal client
            CreateOrderResponse order = await client.CreateOrder(amount);
            if (order == null)
            {
                return Json(new { error = "Failed to create order" });
            }
            // Return the order ID and approval URL
            return Json(new
            {
                id = order.Id,
                approvalUrl = order.Links.FirstOrDefault(link => link.Rel == "approve")?.Href
            });
        }

        [HttpPost]
        public async Task<JsonResult> CaptureOrder([FromBody]JsonObject orderId)
        {
            // Validate the incoming JSON object
            if (!orderId.ContainsKey("orderID") || orderId["orderID"] == null || orderId["orderID"]!.ToString() == null)
            {
                return Json(new { error = "Invalid order ID" });
            }
            // Set the order ID from the JSON object
            string orderIdValue = orderId["orderID"]!.ToString();
            // Capture the order using the PayPal client
            CaptureOrderResponse captureResponse = await client.CaptureOrder(orderIdValue);
            if (captureResponse.Status == "COMPLETED")
            {
                // Handle successful capture
                // You can save the capture response to your database or perform any other actions here
                return Json(new { success = true, captureId = captureResponse.Id, status = captureResponse.Status });
            }
            else
            {
                // Handle failed capture
                return Json(new { success = false, error = "Failed to capture order", status = captureResponse.Status });
            }
        }
    }
}
