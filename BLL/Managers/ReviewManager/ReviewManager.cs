using DAL.Repositories.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.ReviewManager
{
    public class ReviewManager: IReviewManager
    {
        private readonly IReviewRepository reviewRepository;
        public ReviewManager(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public void AddReview(Review review)
        {
            reviewRepository.Add(review);
        }
        public void DeleteReview(Review review)
        {
            reviewRepository.Delete(review);
        }
        public void UpdateReview(Review review)
        {
            reviewRepository.Update(review);
        }
        public async Task<ICollection<Review>> GetAllReviews()
        {
            return await reviewRepository.GetAll();
        }
        public async Task<Review> GetReviewByID(int id)
        {
            return await reviewRepository.GetByID(id);
        }
        public async Task<ICollection<Review>> GetDoctorReviews(int doctorId)
        {
            return await reviewRepository.GetDoctorReviews(doctorId);
        }
        public async Task<float> GetDoctorAverageRating(int docId)
        {
            var reviews = await reviewRepository.GetDoctorReviews(docId);
            if (reviews.Count == 0)
            {
                return 0;
            }
            float sum = 0;
            foreach (var review in reviews)
            {
                sum += review.Rate;
            }
            return sum / reviews.Count;

        }
    }
}
