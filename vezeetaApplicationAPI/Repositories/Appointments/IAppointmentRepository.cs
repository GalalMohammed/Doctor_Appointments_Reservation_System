using DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Appointments
{
    public interface IAppointmentRepository:IGenericRepository<Appointment>
    {
        public Task<int> GetCountByDate(int doctorID, DateTime? date);

    }
}
