using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsApi.Models.DTOs
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ModelDetails
    {
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(254)]
        public string Email { get; set; }
        [MaxLength(12)]
        public string PhoneNo { get; set; }
        [MaxLength(64)]
        public string AddresLine1 { get; set; }
        [MaxLength(64)]
        public string AddresLine2 { get; set; }
        [MaxLength(9)]
        public string Zip { get; set; }
        [MaxLength(64)]
        public string City { get; set; }
        [MaxLength(64)]
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        [MaxLength(64)]
        public string Nationality { get; set; }
        public double Height { get; set; }
        public int ShoeSize { get; set; }
        [MaxLength(32)]
        public string HairColor { get; set; }
        [MaxLength(32)]
        public string EyeColor { get; set; }
        [MaxLength(1000)]
        public string Comments { get; set; }
        [MaxLength(60)]
        public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
