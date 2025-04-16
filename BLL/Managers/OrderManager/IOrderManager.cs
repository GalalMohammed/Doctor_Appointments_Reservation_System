using DAL.Models;

namespace BLLServices.Managers.OrderManager
{
    public interface IOrderManager
    {
        bool IsOrderPaid(int patientId, int doctorReservationId);
        void AddOrder(string id, int patientId, int doctorReservationId);
        void DeleteOrder(int patientId, int doctorReservationId);
        void MarkAsPaid(int patientId, int doctorReservationId);
        Order? GetOrder(int patientId, int doctorReservationId);
        Order? GetOrderById(string orderId);
    }
}
