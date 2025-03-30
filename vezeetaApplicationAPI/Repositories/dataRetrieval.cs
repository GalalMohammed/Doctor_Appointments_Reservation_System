using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;

namespace vezeetaApplicationAPI.Repository
{

    public class dataRetrieval
    {
        private AppDbContext _Context;
        public dataRetrieval(AppDbContext context)
        {
            _Context = context;
        }
        public List<Models.Specialty> GetSpecialties()
        {
            return _Context.Specialties.ToList();
        }
    }
}
