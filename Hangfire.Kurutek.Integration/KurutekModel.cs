using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Hangfire.Kurutek.Integration
{
    public partial class KurutekModel : DbContext
    {
        public KurutekModel()
            : base("name=KurutekContext")
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
