using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiegarniaOnline.DAL
{
    public class KsiegarniaOnlineContext : DbContext
    {
        public KsiegarniaOnlineContext(DbContextOptions<KsiegarniaOnlineContext> options)
         : base(options)
        {
        }


    }
}
