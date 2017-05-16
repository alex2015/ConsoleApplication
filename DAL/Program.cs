using System;
using DAL;


namespace ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new InheritanceMappingContext())
            {
                var ba1 = new BankAccount_TPH
                {
                    Owner = "qqqq",
                    Number = "1",
                    BankName = "www",
                    Swift = "eeeee"
                };

                var ba2 = new BankAccount_TPH
                {
                    Owner = "aaaaa",
                    Number = "2",
                    BankName = "ssssss",
                    Swift = "ddddd"
                };

                var cr1 = new CreditCard_TPH
                {
                    Owner = "zzzz",
                    Number = "3",
                    CardType = 1,
                    ExpiryMonth = "1",
                    ExpiryYear = "2017"
                };

                var cr2 = new CreditCard_TPH
                {
                    Owner = "xxxxx",
                    Number = "4",
                    CardType = 2,
                    ExpiryMonth = "2",
                    ExpiryYear = "2017"
                };

                db.BillingDetail_TPHs.Add(ba1);
                db.BillingDetail_TPHs.Add(ba2);
                db.BillingDetail_TPHs.Add(cr1);
                db.BillingDetail_TPHs.Add(cr2);

                db.SaveChanges();

                foreach (var bd in db.BillingDetail_TPHs)
                {
                    Console.WriteLine("{0}", bd.Number);
                }
            }

            Console.ReadKey();
        }

    }
}
