using Stripe.Checkout;

namespace BLLServices.Common.PaymentService
{
    public interface IPaymentService
    {
        Task<Session> Pay(int doctorId, string host);
    }
}
