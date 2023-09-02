using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;

namespace DOAN.Entities
{
    [Table("Pay")]
    public class PayEntity
    {
        [Key]
        public int Paymentid { set; get; }
        public string PaymentName { set; get; }
    }
}
