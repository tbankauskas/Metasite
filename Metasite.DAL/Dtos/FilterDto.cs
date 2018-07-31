using System;

namespace Metasite.DAL.Dtos
{
    public class FilterDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Type { get; set; }
        public bool FilterTop5 { get; set; }
    }
}
