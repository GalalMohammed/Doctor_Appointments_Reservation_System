using DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.DoctorReservations
{
    public interface IDoctorReservationRepository : IGenericRepository<DoctorReservation>
    {
        public Task<List<DoctorReservation>> GetReservationsByDocID(int id, bool WithAsNoTracking = true);


    }


}
