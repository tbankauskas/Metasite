using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metasite.DAL.Entities
{
    [Table("EventType")]
    public class EventType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventTypeId { get; set; }
        public string Type { get; set; }
        public ICollection<EventLog> EventLogs { get; set; }
    }
}
