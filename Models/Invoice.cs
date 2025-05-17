using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupermarketWEB.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public int number { get; set; }

        [Required]
        public int customerId { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public int paymodeId { get; set; }

        [ForeignKey("customerId")]
        public Customer? Customer  { get; set; }

        [ForeignKey("paymodeId")]
        public PayMode? PayMode { get; set; }
     
        public ICollection<Detail>? Details { get; set; }
    }
}
