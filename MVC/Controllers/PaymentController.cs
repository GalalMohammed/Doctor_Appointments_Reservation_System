using BLLServices.Payment;
using BLLServices.Payment.DTOs;
using BLLServices.Common.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.OrderManager;
using DAL.Models;
using BLLServices.Managers.AppointmentManager;
using vezeetaApplicationAPI.Models;
using BLLServices.Common.ReCaptchaService;

namespace MVC.Controllers
{
    public class PaymentController(PayPalClient client, IPaymentService paymentService, IDoctorReservationManager reservationManager, IOrderManager orderManager, ReCaptchaService reCaptcha) : Controller
    {
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Index()
        {
            if (Request.Method == "POST")
            {
                // Handle the form submission here
                string googleReCaptchaResponse = Request.Form["g-recaptcha-response"].ToString();
                // Verify the token
                bool isValidCaptcha = await reCaptcha.ValidateReCaptcha(googleReCaptchaResponse);
            }

            ViewBag.ClientId = client.ClientId;
            ViewBag.amount = 100;
            return View();
            
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrder([FromBody] JsonObject data)
        {
            // Validate the incoming JSON object
            if (!data.ContainsKey("reservationId") || data["reservationId"] == null || int.TryParse(data["reservationId"]!.ToString(), out int reservationId) == false)
                return Json(new { error = "Invalid reservationId" });
            
            var IsParsed = int.TryParse(User.FindFirst("currentId")?.Value, out int patientId);
            

            Order? trackedOrder = orderManager.GetOrder(patientId, reservationId);
            if (trackedOrder != null && trackedOrder.Status)
                return Json(new { error = "Your appointment is already paid" });
            if (trackedOrder != null)
                orderManager.DeleteOrder(patientId, reservationId);
            // Set the amount to be charged
            int amount = (await reservationManager.GetDoctorReservationByID(reservationId)).Doctor?.Fees ?? throw new Exception("Doctor Navigation Property is null");
            //Create an order using the PayPal client
            CreateOrderResponse order = await client.CreateOrder(amount.ToString());
            if (order == null)
                return Json(new { error = "Failed to create order" });
            // Create a new order in the database
            orderManager.AddOrder(order.Id, patientId, reservationId);
            //Return the order ID and approval URL
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
                return Json(new { error = "Invalid order ID" });
            // Set the order ID from the JSON object
            string orderIdValue = orderId["orderID"]!.ToString();
            // Capture the order using the PayPal client
            CaptureOrderResponse captureResponse = await client.CaptureOrder(orderIdValue);
            if (captureResponse.Status == "COMPLETED")
            {
                // Handle successful capture
                Order trackedOrder = orderManager.GetOrderById(orderIdValue)
                    ?? throw new Exception("Order not found");
                if (!int.TryParse(User.FindFirst("currentId")?.Value, out int patientId))
                    return Json(new { error = "Invalid patient ID" });
                orderManager.MarkAsPaid(patientId, trackedOrder.DoctorReservationId);
                return Json(new { success = true, captureId = captureResponse.Id, status = captureResponse.Status, patientId });
            }
            else
                // Handle failed capture
                return Json(new { success = false, error = "Failed to capture order", status = captureResponse.Status });
        }
        [HttpPost("checkout")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(int doctorId)
        {
            var session = await paymentService.Pay(doctorId, Request.Host.ToString());
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}