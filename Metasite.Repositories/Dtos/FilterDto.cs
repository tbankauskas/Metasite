using System;

namespace Metasite.Repositories.Dtos
{
    public class FilterDto
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Type { get; set; }
    }
}
