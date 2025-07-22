using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsWithCSharp.ChainOfResponsibility
{
    public class BankTransaction
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string DestinationAccount { get; set; }
    }
    public class BankTransitionValidation
    {
        public readonly List<Func<BankTransaction, bool>> _rules = new();
        public BankTransitionValidation AddRules(Func<BankTransaction,bool> rule)
        {
            _rules.Add(rule);
            return this;
        }

        public bool Validate(BankTransaction tx)
        {
            foreach(var rule in _rules)
            {
                if (!rule(tx))
                   { 
                    Console.WriteLine("Transition bloled.");
                    return false;
                }
            }
            Console.WriteLine("Transition approved.");
            return true;
        }
    }

    public class HandleBankTransition
    {
        public static void Run()
        {
            var blacklistedAccounts = new List<string> { "9999", "1234" };
            var txValidator = new BankTransitionValidation()
                .AddRules(tx => tx.Amount <= tx.Balance)
                .AddRules(tx => tx.Amount <= 10000)
                .AddRules(tx => blacklistedAccounts.Contains(tx.DestinationAccount));

            var tx1 = new BankTransaction { Amount = 5000, Balance = 6000, DestinationAccount = "5678" };
            var tx2 = new BankTransaction { Amount = 15000, Balance = 6000, DestinationAccount = "1234" };

            txValidator.Validate(tx1);
            txValidator.Validate(tx2);
        }
    }
}
