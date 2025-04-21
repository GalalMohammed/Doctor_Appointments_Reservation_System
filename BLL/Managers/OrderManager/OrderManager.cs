using DAL.Models;
using DAL.Repositories.Orders;
using Microsoft.Extensions.Logging;

namespace BLLServices.Managers.OrderManager
{
    public class OrderManager(IOrderRepository orderRepository) : IOrderManager
    {
        public void AddOrder(string id, int patientId, int doctorReservationId)
            => orderRepository.Add(new Order() { Id = id, PatientId = patientId, DoctorReservationId = doctorReservationId, Status = false });

        public void DeleteOrder(int patientId, int doctorReservationId)
            => orderRepository.Delete(orderRepository.GetOrder(patientId, doctorReservationId));

        public Order? GetOrder(int patientId, int doctorReservationId)
        {
            return orderRepository.GetOrder(patientId, doctorReservationId);
        }

        public Order? GetOrderById(string orderId) => orderRepository.GetByID(orderId).Result;

        //=> orderRepository.GetOrder(patientId, doctorReservationId)?.Status == true;
        public bool IsOrderPaid(int patientId, int doctorReservationId)
        {
            var order = orderRepository.GetOrder(patientId, doctorReservationId);
            return order != null && order.Status;
        }

        public void MarkAsPaid(int patientId, int doctorReservationId)
        {
            var order = orderRepository.GetOrder(patientId, doctorReservationId);
            order.Status = true;
            orderRepository.Update(order);
        }
        public void SetOrderCaptureId(string orderId, string captureId)
        {
            var order = orderRepository.GetByID(orderId).Result;
            if (order != null)
            {
                order.CaptureId = captureId;
                orderRepository.Update(order);
            }
        }
    }
}
