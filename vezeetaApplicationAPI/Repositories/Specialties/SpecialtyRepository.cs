using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Specialties
{
    internal class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
    {
        private readonly AppDbContext context;
        public SpecialtyRepository(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public override async Task<List<Specialty>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Specialties.Include(s=>s.Doctors).AsNoTracking()
                    .ToListAsync();
            }
            return await context.Specialties.Include(s => s.Doctors).ToListAsync();
        }
        public override async Task<Specialty> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Specialties.Include(s => s.Doctors).Where(s => s.ID == id)
                .FirstOrDefaultAsync();
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }
    }
}
