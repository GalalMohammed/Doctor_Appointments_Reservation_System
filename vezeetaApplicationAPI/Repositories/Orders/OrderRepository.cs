using DAL.Models;
using DAL.Repositories.Generic;
using vezeetaApplicationAPI.DataAccess;

namespace DAL.Repositories.Orders
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }
        public Order? GetOrder(int patientId, int doctorReservationId)
            => context.Orders.Where(o => o.PatientId == patientId && o.DoctorReservationId == doctorReservationId).FirstOrDefault();
    }
}
