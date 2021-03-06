﻿using System;
using System.Diagnostics;
using DAL;


namespace ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TPH
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

            //TPT
            using (var context = new InheritanceMappingContext())
            {
                var creditCard = new CreditCard_TPT
                {
                    Number = "987654321",
                    CardType = 1
                };
                var user = new User_TPT
                {
                    UserId = 1,
                    BillingInfo = creditCard
                };
                context.User_TPTs.Add(user);
                context.SaveChanges();
            }

            using (var context = new InheritanceMappingContext())
            {
                var user = context.User_TPTs.Find(1);
                Debug.Assert(user.BillingInfo is CreditCard_TPT);
            }

            //TPC

            using (var context = new InheritanceMappingContext())
            {
                var bankAccount = new BankAccount_TPC
                {
                    BillingDetailId = 1
                };
                var creditCard = new CreditCard_TPC
                {
                    BillingDetailId = 2,
                    CardType = 1
                };

                context.BillingDetail_TPCs.Add(bankAccount);
                context.BillingDetail_TPCs.Add(creditCard);

                context.SaveChanges();
            }

            Console.ReadKey();
        }

    }
}
