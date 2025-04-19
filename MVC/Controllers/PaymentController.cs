using BLLServices.Common.PaymentService;
using BLLServices.Common.ReCaptchaService;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.OrderManager;
using BLLServices.Payment;
using BLLServices.Payment.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace MVC.Controllers
{
    [Authorize(Roles = "patient")]
    public class PaymentController(PayPalClient client, IPaymentService paymentService, IDoctorReservationManager reservationManager, IOrderManager orderManager, ReCaptchaService reCaptcha) : Controller
    {

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
        public async Task<JsonResult> CaptureOrder([FromBody] JsonObject orderId)
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
                // Set the capture ID in the order
                orderManager.SetOrderCaptureId(orderIdValue, captureResponse.CaptureId);
                orderManager.MarkAsPaid(patientId, trackedOrder.DoctorReservationId);
                return Json(new { success = true, captureId = captureResponse.Id, status = captureResponse.Status, patientId });
            }
            else
                // Handle failed capture
                return Json(new { success = false, error = "Failed to capture order", status = captureResponse.Status });
        }
        [HttpPost]
        public async Task<JsonResult> IsBooked([FromBody] JsonObject data)
        {
            if (!data.ContainsKey("reservationId") || data["reservationId"] == null)
                return Json(new { error = "Invalid order ID" });
            if (int.TryParse(data["reservationId"].ToString(), out int resId))
            {
                var order = orderManager.GetOrder(int.Parse(User.FindFirst("currentId").Value), resId);
                if (order == null || !order.Status)
                    return Json(new { success = true });
                return Json(new { success = false });
            }
            return Json(new { success = false });
        }
        //[HttpPost("checkout")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Checkout(int doctorId)
        //{
        //    var session = await paymentService.Pay(doctorId, Request.Host.ToString());
        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);
        //}
    }
}