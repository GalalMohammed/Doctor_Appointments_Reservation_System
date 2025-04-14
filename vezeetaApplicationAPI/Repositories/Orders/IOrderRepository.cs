using DAL.Models;
using DAL.Repositories.Generic;

namespace DAL.Repositories.Orders
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Order? GetOrder(int patientId, int doctorReservationId);
    }
}
