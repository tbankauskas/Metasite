using Metasite.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metasite.DAL
{
    public class MSContext : DbContext
    {
        public MSContext(DbContextOptions<MSContext> options) : base(options)
        {
        }
        public virtual DbSet<MsIsdn> MsIsdns { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }
    }
}
