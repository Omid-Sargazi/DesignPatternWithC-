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

    public class IncomeManagment
    {
        public static void Execute()
        {
            var categories = new List<Category>
        {
            new Category { Name = "Food", MonthlyBudget = 3000000, Color = "Green" },
            new Category { Name = "Transport", MonthlyBudget = 1000000, Color = "Blue" },
            new Category { Name = "Entertainment", MonthlyBudget = 500000, Color = "Purple" },
            new Category { Name = "Bills", MonthlyBudget = 2000000, Color = "Red" },
            new Category { Name = "Shopping", MonthlyBudget = 1500000, Color = "Yellow" },
            new Category { Name = "Healthcare", MonthlyBudget = 800000, Color = "Pink" }
        };

            // درآمدها
            var incomes = new List<Income>
        {
            new Income { Id = 1, Source = "Salary", Amount = 10000000, Date = new DateTime(2024, 1, 1), IsRegular = true },
            new Income { Id = 2, Source = "Freelance", Amount = 2000000, Date = new DateTime(2024, 1, 15), IsRegular = false },
            new Income { Id = 3, Source = "Salary", Amount = 10000000, Date = new DateTime(2024, 2, 1), IsRegular = true }
        };

            // مخارج (داده‌های نمونه برای 2 ماه گذشته و جاری)
            var expenses = new List<Expense>
        {
            // January Expenses
            new Expense { Id = 1, Description = "Grocery shopping", Category = "Food", Amount = 450000,
                         Date = new DateTime(2024, 1, 5), PaymentMethod = "Card", IsRecurring = true },
            new Expense { Id = 2, Description = "Taxi to work", Category = "Transport", Amount = 120000,
                         Date = new DateTime(2024, 1, 6), PaymentMethod = "Cash", IsRecurring = true },
            new Expense { Id = 3, Description = "Movie tickets", Category = "Entertainment", Amount = 80000,
                         Date = new DateTime(2024, 1, 10), PaymentMethod = "Card", IsRecurring = false },
            new Expense { Id = 4, Description = "Electricity bill", Category = "Bills", Amount = 350000,
                         Date = new DateTime(2024, 1, 15), PaymentMethod = "Online", IsRecurring = true },
            new Expense { Id = 5, Description = "New shoes", Category = "Shopping", Amount = 600000,
                         Date = new DateTime(2024, 1, 20), PaymentMethod = "Card", IsRecurring = false },
            
            // February Expenses
            new Expense { Id = 6, Description = "Restaurant dinner", Category = "Food", Amount = 250000,
                         Date = new DateTime(2024, 2, 3), PaymentMethod = "Card", IsRecurring = false },
            new Expense { Id = 7, Description = "Bus pass", Category = "Transport", Amount = 200000,
                         Date = new DateTime(2024, 2, 5), PaymentMethod = "Cash", IsRecurring = true },
            new Expense { Id = 8, Description = "Gym membership", Category = "Healthcare", Amount = 300000,
                         Date = new DateTime(2024, 2, 10), PaymentMethod = "Online", IsRecurring = true },
            new Expense { Id = 9, Description = "Internet bill", Category = "Bills", Amount = 180000,
                         Date = new DateTime(2024, 2, 12), PaymentMethod = "Online", IsRecurring = true },
            new Expense { Id = 10, Description = "Concert tickets", Category = "Entertainment", Amount = 400000,
                         Date = new DateTime(2024, 2, 15), PaymentMethod = "Card", IsRecurring = false },
            
            // March (current month) Expenses
            new Expense { Id = 11, Description = "Supermarket", Category = "Food", Amount = 380000,
                         Date = DateTime.Now.AddDays(-5), PaymentMethod = "Card", IsRecurring = true },
            new Expense { Id = 12, Description = "Gasoline", Category = "Transport", Amount = 350000,
                         Date = DateTime.Now.AddDays(-3), PaymentMethod = "Card", IsRecurring = true },
            new Expense { Id = 13, Description = "Pharmacy", Category = "Healthcare", Amount = 120000,
                         Date = DateTime.Now.AddDays(-2), PaymentMethod = "Cash", IsRecurring = false },
            new Expense { Id = 14, Description = "Water bill", Category = "Bills", Amount = 90000,
                         Date = DateTime.Now.AddDays(-1), PaymentMethod = "Online", IsRecurring = true }
        };
        }
    }
}
