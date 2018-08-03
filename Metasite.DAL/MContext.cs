using Metasite.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metasite.DAL
{
    public class MContext : DbContext
    {
        public MContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<MsIsdn> MsIsdns { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }
    }
}
