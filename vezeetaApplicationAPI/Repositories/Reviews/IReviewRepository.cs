using DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Reviews
{
    internal interface IReviewRepository : IGenericRepository<Review>
    {
        public Task<ICollection<Review>> GetDoctorReviews(int doctorId, bool WithAsNoTracking = true);

    }
}
