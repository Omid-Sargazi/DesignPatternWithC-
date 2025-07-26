using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ChainOfResponsibility
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string DestinationAccount { get; set; }
    }
    public class TransactionValidator
    {
        private readonly List<Func<Transaction,bool>> _rules = new();
        public TransactionValidator AddRule(Func<Transaction, bool> rule)
        {
            _rules.Add(rule);
            return this;
        }

        public bool Validate(Transaction transaction)
        {
            foreach(var rule in _rules)
            {
                if (!rule(transaction))
                {
                    Console.WriteLine($"Transaction Blocled");
                    return false;
                }
            }
                    Console.WriteLine($"Transaction approved");
            return true;

        }
    }

    public class ClientRunTransaction
    {
        public static void Run()
        {
            var blacklistedAccounts = new List<string> { "9999", "1234" };

            var tx1 = new Transaction { Amount = 5000, Balance = 6000, DestinationAccount = "5678" };
            var tx2 = new Transaction { Amount = 15000, Balance = 6000, DestinationAccount = "1234" };

            var txValidator = new TransactionValidator()
                .AddRule(tx => tx.Amount <= tx1.Balance)
                .AddRule(tx => tx.Amount <= 1000)
                .AddRule(tx => blacklistedAccounts.Contains(tx.DestinationAccount));

            txValidator.Validate(tx1);
            txValidator.Validate(tx2);
        }
    }
}
