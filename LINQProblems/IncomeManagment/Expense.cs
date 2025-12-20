using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.IncomeManagment
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // "Food", "Transport", "Entertainment", "Bills"
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; } // "Cash", "Card", "Online"
        public bool IsRecurring { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public decimal MonthlyBudget { get; set; }
        public string Color { get; set; } // For UI purposes
    }

    public class Income
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRegular { get; set; }
    }
}
