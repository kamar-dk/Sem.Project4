using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsApi.Models.Entities
{
    public class EfModel
    {
        public long EfModelId { get; set; }
        public long EfAccountId { get; set; }
        public EfAccount? Account { get; set; }
        [MaxLength(64)]
        public string? FirstName { get; set; }
        [MaxLength(32)]
        public string? LastName { get; set; }
        [MaxLength(254)]
        public string? Email { get; set; }
        [MaxLength(12)]
        public string? PhoneNo { get; set; }
        [MaxLength(64)]
        public string? AddresLine1 { get; set; }
        [MaxLength(64)]
        public string? AddresLine2 { get; set; }
        [MaxLength(9)]
        public string? Zip { get; set; }
        [MaxLength(64)]
        public string? City { get; set; }
        [MaxLength(64)]
        public string? Country { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [MaxLength(64)]
        public string? Nationality { get; set; }
        public double Height { get; set; }
        public int ShoeSize { get; set; }
        [MaxLength(32)]
        public string? HairColor { get; set; }
        [MaxLength(32)]
        public string? EyeColor { get; set; }
        [MaxLength(1000)]
        public string? Comments { get; set; }

        public List<EfJobModel>? JobModels { get; set; }
        public List<EfExpense>? Expenses { get; set; }
    }
}
