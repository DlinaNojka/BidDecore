using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidDecore.Models
{
    public class Lot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LotId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public decimal StartPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public string Category { get; set; }

        public byte[]? ImageData { get; set; }

        public User User { get; set; }
    }
}
