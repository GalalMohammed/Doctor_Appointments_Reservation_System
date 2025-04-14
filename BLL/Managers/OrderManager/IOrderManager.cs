namespace BLLServices.Managers.OrderManager
{
    public interface IOrderManager
    {
        Task<bool> IsOrderPaid(int patientId, int doctorReservationId);
        void AddOrder(int patientId, int doctorReservationId);
        void DeleteOrder(int patientId, int doctorReservationId);
        void MarkAsPaid(int patientId, int doctorReservationId);
    }
}
