namespace BLLServices.Common.CancelationService
{
    public interface ICancelationService
    {
        public Task CancelDoctorReservation(int resId);
    }
}
