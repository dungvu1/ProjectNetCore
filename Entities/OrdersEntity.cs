using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace DOAN.Entities
{
    [Table("Orders")]
    public class OrdersEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int  OrderId { set; get; }
        public int UsersId { set; get; }
        public DateTime OrderDate { set; get; }
        public string FullName { set; get; }
        public int Phone { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public int Paymentid { set; get; }
        public int StatusId { set; get; }
        public bool IsDeleted { set; get; }
        public string Note { set; get; }
        public int ToTalOrder { set; get; }
    }
}
