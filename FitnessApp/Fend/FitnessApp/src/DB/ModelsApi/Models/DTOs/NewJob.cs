using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsApi.Models.DTOs
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class NewJob
    {
        [MaxLength(64)]
        public string Customer { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string Location { get; set; }
        [MaxLength(2000)]
        public string Comments { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
