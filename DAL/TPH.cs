using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public abstract class BillingDetail_TPH
    {
        [Key]
        public int BillingDetailId { get; set; }
        public string Owner { get; set; }
        public string Number { get; set; }
    }

    public class BankAccount_TPH : BillingDetail_TPH
    {
        public string BankName { get; set; }
        public string Swift { get; set; }
    }

    public class CreditCard_TPH : BillingDetail_TPH
    {
        public int CardType { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}
