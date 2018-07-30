using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Metasite.DAL.Entities
{
    [Table("MsIsdn")]
    public class MsIsdn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MsIsdnId { get; set; }
        [StringLength(11)]
        public string MsIsdnNumber { get; set; }
        public ICollection<EventLog> EventLogs { get; set; }
    }
}
