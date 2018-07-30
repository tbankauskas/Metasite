using Metasite.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metasite.DAL
{
    public class MSContext : DbContext
    {
        public MSContext(DbContextOptions<MSContext> options) : base(options)
        {
        }
        public DbSet<MsIsdn> MsIsdns { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
    }
}
