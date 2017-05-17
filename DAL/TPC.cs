using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public abstract class BillingDetail_TPC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillingDetailId { get; set; }
        public string Owner { get; set; }
        public string Number { get; set; }
    }

    public class BankAccount_TPC : BillingDetail_TPC
    {
        public string BankName { get; set; }
        public string Swift { get; set; }
    }

    public class CreditCard_TPC : BillingDetail_TPC
    {
        public int CardType { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}
