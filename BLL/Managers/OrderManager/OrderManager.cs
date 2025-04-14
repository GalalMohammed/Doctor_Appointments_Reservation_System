using DAL.Models;
using DAL.Repositories.Orders;

namespace BLLServices.Managers.OrderManager
{
    public class OrderManager(IOrderRepository orderRepository) : IOrderManager
    {
        public void AddOrder(int patientId, int doctorReservationId)
            => orderRepository.Add(new Order() { PatientId = patientId, DoctorReservationId = doctorReservationId, Status = false });

        public void DeleteOrder(int patientId, int doctorReservationId)
            => orderRepository.Delete(orderRepository.GetOrder(patientId, doctorReservationId));

        public Order? GetOrder(int patientId, int doctorReservationId)
        {
            return orderRepository.GetOrder(patientId, doctorReservationId);
        }

        public Order? GetOrderById(int orderId) => orderRepository.GetByID(orderId).Result;

        public async Task<bool> IsOrderPaid(int patientId, int doctorReservationId)
            => orderRepository.GetOrder(patientId, doctorReservationId).Status;

        public void MarkAsPaid(int patientId, int doctorReservationId)
        {
            var order = orderRepository.GetOrder(patientId, doctorReservationId);
            order.Status = true;
            orderRepository.Update(order);
        }
    }
}
