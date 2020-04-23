using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LocateMissileProjectContext : DbContext
    {
        public DbSet<Fall> Falls { get; set; }
        public DbSet<FallReport> Reports { get; set; }
        public DbSet<FallPrediction> Predictions { get; set; }
    }
}
