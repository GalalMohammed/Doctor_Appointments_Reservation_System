using DAL.Models;
using DAL.Repositories.Orders;

namespace BLLServices.Managers.OrderManager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public void AddOrder(int patientId, int doctorReservationId)
            => orderRepository.Add(new Order() { PatientId = patientId, DoctorReservationId = doctorReservationId, Status = false });

        public void DeleteOrder(int patientId, int doctorReservationId)
            => orderRepository.Delete(orderRepository.GetOrder(patientId, doctorReservationId));

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
