using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.ReviewManager
{
    public interface IReviewManager 
    {
        void AddReview(Review review);
        void DeleteReview(Review review);
        void UpdateReview(Review review);
        Task<ICollection<Review>> GetAllReviews();
        Task<Review> GetReviewByID(int id);
        Task<ICollection<Review>> GetDoctorReviews(int doctorId);
    }
}
