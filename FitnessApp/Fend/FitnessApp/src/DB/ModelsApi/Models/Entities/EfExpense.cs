using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsApi.Models.Entities
{
    public class EfExpense
    {
        public long EfExpenseId { get; set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal amount { get; set; }
    }
}
