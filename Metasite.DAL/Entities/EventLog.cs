using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Metasite.DAL.Entities
{
    [Table("EventLog")]
    public class EventLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventLogId { get; set; }
        public int MsIsdnId { get; set; }
        public int EventTypeId { get; set; }
        public int? Duration { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual MsIsdn MsIsdn { get; set; }
        public virtual EventType EventType { get; set; }

    }
}
