using BLLServices.Common.PaymentService;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    //[Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
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