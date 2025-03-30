using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Reviews
{
    internal class ReviewRepository : GenericRepository<Review> , IReviewRepository
    {
        private readonly AppDbContext context;
        public ReviewRepository(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public override async Task<List<Review>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Reviews.Include(r=>r.Patient).Include(r=>r.Doctor)
                    .AsNoTracking().ToListAsync();
            }
            return await context.Reviews.Include(r => r.Patient).Include(r => r.Doctor)
                    .ToListAsync();
        }
        public override async Task<Review> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Reviews.Include(r => r.Patient).Include(r => r.Doctor)
                    .Where(r=>r.ID==id).FirstOrDefaultAsync();
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }
    }
}
