namespace ModelsApi.Models.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    namespace ModelsApi.Models.DTOs
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public class ModelNoJobs
        {
            [MaxLength(64)]
            public string FirstName { get; set; }
            [MaxLength(32)]
            public string LastName { get; set; }
            [MaxLength(254)]
            public string Email { get; set; }
            [MaxLength(12)]
            public string PhoneNo { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        }
    }
}
