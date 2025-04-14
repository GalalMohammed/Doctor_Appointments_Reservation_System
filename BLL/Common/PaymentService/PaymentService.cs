using BLLServices.Managers.DoctorManger;
using Stripe;
using Stripe.Checkout;

namespace BLLServices.Common.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IDoctorManager doctorManager;

        public PaymentService(IDoctorManager doctorManager)
        {
            StripeConfiguration.ApiKey = "sk_test_51RD3NvP0oRro1bwp0Q99yAyex84nhNWd3BocqiPLI6RnuRdRcahyxR5VFKDGRobNPENxtUlSOiA4ohkRcCsfNSNP00nK2nNkal";
            this.doctorManager = doctorManager;
        }
        public async Task<Session> Pay(int doctorId, string host)
        {
            var doctor = await doctorManager.GetDoctorByID(doctorId);
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                    {
                      new SessionLineItemOptions
                      {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                          UnitAmount = doctor.Fees * 1000,
                          Currency = "egp",
                          ProductData = new SessionLineItemPriceDataProductDataOptions
                          {
                            Name = "New Appointment",
                            Description = $"A {doctor.Specialty.Name} appointment with Dr. {doctor.FirstName} {doctor.LastName}"
                          },
                        },
                        Quantity = 1,
                      },
                    },
                Mode = "payment",
                SuccessUrl = $"http://{host}",
                CancelUrl = $"http://{host}/account/register",
            };
            var service = new SessionService();
            return service.Create(options);
        }
    }
}
