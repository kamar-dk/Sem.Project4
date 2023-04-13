using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsApi.Models.Entities
{
    public class EfJob
    {
        public long EfJobId { get; set; }
        [MaxLength(64)]
        public string? Customer { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }

        public List<EfJobModel>? JobModels { get; set; }
    }
}
