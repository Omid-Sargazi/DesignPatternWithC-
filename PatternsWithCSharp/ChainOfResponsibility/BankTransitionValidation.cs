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
}
