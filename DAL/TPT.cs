using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class User_TPT
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BillingDetailId { get; set; }

        public virtual BillingDetail_TPT BillingInfo { get; set; }
    }

    public abstract class BillingDetail_TPT
    {
        [Key]
        public int BillingDetailId { get; set; }
        public string Owner { get; set; }
        public string Number { get; set; }
    }

    [Table("BankAccount_TPT")]
    public class BankAccount_TPT : BillingDetail_TPT
    {
        public string BankName { get; set; }
        public string Swift { get; set; }
    }

    [Table("CreditCard_TPT")]
    public class CreditCard_TPT : BillingDetail_TPT
    {
        public int CardType { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}
