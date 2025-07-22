using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PatternsWithCSharp.ChainOfResponsibility
{
    public enum ValidationResult
    {
        Valid,
        InvalidName,
        InvalidEmail,
        WeakPassword
    }

    public class Transaction
    {
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal DailyLimit { get; set; }
        public bool IsBlacklisted { get; set; }
    }

    public class TransactionResult
    {
        public List<string> Errors { get; } = new();
        public bool IsSuccess =>Errors.Count == 0;
    }

    public abstract class TransitionHandler
    {
        protected TransitionHandler _next;
        protected TransitionHandler setNext(TransitionHandler next)
        {
            _next = next; 
            return this;
        }

        public abstract void Handle(Transaction transaction, TransactionResult result);

        public void Execute(Transaction transaction, TransactionResult result)
        {
            Console.WriteLine($"[{this.GetType().Name}] is checking...");
            _next?.Execute(transaction, result);
        }
    }

    public class BalanceCheckHandler : TransitionHandler
    {
        public override void Handle(Transaction transaction, TransactionResult result)
        {
            if(transaction.Amount>transaction.DailyLimit)
            {
                result.Errors.Add("limition in transition");
            }
        }
    }

    public class BlacklistHandler : TransitionHandler
    {
        public override void Handle(Transaction transaction, TransactionResult result)
        {
           if(transaction.IsBlacklisted)
            {
                result.Errors.Add("user is blocked.");
            }
        }
    }

    public class DailyLimitHandler : TransitionHandler
    {
        public override void Handle(Transaction transaction, TransactionResult result)
        {
            if (transaction.Amount > transaction.DailyLimit)
                result.Errors.Add("limitions in transition");
        }
    }

    public class HandlerTransition
    {
        public static void Run()
        {
            var balance = new BalanceCheckHandler();
            var limit = new DailyLimitHandler();
            var blackList = new BlacklistHandler();

        }
    }
}
