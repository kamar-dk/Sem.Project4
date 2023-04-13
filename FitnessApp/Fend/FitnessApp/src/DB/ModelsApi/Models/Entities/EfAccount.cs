using System.ComponentModel.DataAnnotations;

namespace ModelsApi.Models.Entities
{
    public class EfAccount
    {
        [Key]
        public long EfAccountId { get; set; }
        [MaxLength(254)]
        public string Email { get; set; }
        [MaxLength(60)]
        public string PwHash { get; set; }        
        public bool IsManager { get; set; }
    }
}
