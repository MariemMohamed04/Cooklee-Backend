using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class ChefPageRepo : GenericRepo<ChefPage>, IChefPageRepo
    {
        public ChefPageRepo(CookleeDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
