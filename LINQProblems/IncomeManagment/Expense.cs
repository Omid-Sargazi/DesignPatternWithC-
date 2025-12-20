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

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var currentMonthExpenses = expenses
                .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear)
                .OrderByDescending(e => e.Amount)
                .Select(e => new
                {
                    e.Description,
                    e.Category,
                    e.Amount,
                    e.Date,
                    e.PaymentMethod,
                    DaysAgo = (DateTime.Now - e.Date).Days
                })
                .ToList();

            Console.WriteLine($"=== Current Month Expenses ({DateTime.Now:MMMM}) ===");
            foreach (var expense in currentMonthExpenses)
            {
                Console.WriteLine($"{expense.Date:dd MMM}: {expense.Description}");
                Console.WriteLine($"  Category: {expense.Category}, Amount: {expense.Amount:C0}");
                Console.WriteLine($"  Payment: {expense.PaymentMethod}, {expense.DaysAgo} days ago");
            }

            var expensesByCategory = expenses
                .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear)
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(e => e.Amount),
                    Count = g.Count(),
                    AverageAmount = Math.Round(g.Average(e => e.Amount), 0),
                    Budget = categories.FirstOrDefault(c => c.Name == g.Key)?.MonthlyBudget ?? 0
                })
                .Select(x => new
                {
                    x.Category,
                    x.TotalAmount,
                    x.Count,
                    x.AverageAmount,
                    x.Budget,
                    BudgetUsed = Math.Round(x.TotalAmount / x.Budget * 100, 1),
                    //Status = x.TotalAmount > x.Budget ? "OVER BUDGET" :
                    //    x.TotalAmount > x.Budget * 0.8 ? "WARNING" : "OK"
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

            Console.WriteLine("\n=== Expenses by Category (Current Month) ===");
            foreach (var category in expensesByCategory)
            {
                Console.WriteLine($"{category.Category}:");
                Console.WriteLine($"  Spent: {category.TotalAmount:C0} ({category.Count} expenses)");
                Console.WriteLine($"  Avg per expense: {category.AverageAmount:C0}");
                Console.WriteLine($"  Budget: {category.Budget:C0}, Used: {category.BudgetUsed}%");
                //Console.WriteLine($"  Status: {category.Status}");
            }

            var budgetRemaining = categories
                .Select(c => new
                {
                    c.Name,
                    c.MonthlyBudget,
                    Spent = expenses
                        .Where(e => e.Category == c.Name &&
                                    e.Date.Month == currentMonth &&
                                    e.Date.Year == currentYear)
                        .Sum(e => e.Amount),
                    DailyBudget = c.MonthlyBudget / DateTime.DaysInMonth(currentYear, currentMonth),
                    DaysPassed = DateTime.Now.Day,
                    DaysRemaining = DateTime.DaysInMonth(currentYear, currentMonth) - DateTime.Now.Day
                })
                .Select(x => new
                {
                    x.Name,
                    x.MonthlyBudget,
                    x.Spent,
                    Remaining = x.MonthlyBudget - x.Spent,
                    DailyAverageSpent = Math.Round(x.Spent / x.DaysPassed, 0),
                    ProjectedMonthEnd = Math.Round(x.Spent / x.DaysPassed * DateTime.DaysInMonth(currentYear, currentMonth), 0)
                })
                .OrderBy(x => x.Remaining)
                .ToList();

            Console.WriteLine("\n=== Budget Remaining ===");
            foreach (var budget in budgetRemaining)
            {
                string trend = budget.ProjectedMonthEnd > budget.MonthlyBudget ? "📈 OVER" : "📉 UNDER";
                Console.WriteLine($"{budget.Name}:");
                Console.WriteLine($"  Budget: {budget.MonthlyBudget:C0}, Spent: {budget.Spent:C0}");
                Console.WriteLine($"  Remaining: {budget.Remaining:C0}");
                Console.WriteLine($"  Daily Avg: {budget.DailyAverageSpent:C0}");
                Console.WriteLine($"  Projected: {budget.ProjectedMonthEnd:C0} {trend}");
            }
            var monthlyTrends = expenses
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalExpenses = g.Sum(e => e.Amount),
                    ExpenseCount = g.Count(),
                    AveragePerExpense = Math.Round(g.Average(e => e.Amount), 0),
                    Categories = g.GroupBy(e => e.Category)
                        .Select(cg => new
                        {
                            Category = cg.Key,
                            Amount = cg.Sum(e => e.Amount)
                        })
                        .OrderByDescending(c => c.Amount)
                        .FirstOrDefault()?.Category ?? "N/A"
                })
                .OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.Month)
                .Take(3)
                .ToList();

            Console.WriteLine("\n=== Monthly Expense Trends ===");
            foreach (var month in monthlyTrends)
            {
                Console.WriteLine($"{month.Year}-{month.Month:D2}:");
                Console.WriteLine($"  Total: {month.TotalExpenses:C0}");
                Console.WriteLine($"  Expenses: {month.ExpenseCount}, Avg: {month.AveragePerExpense:C0}");
                Console.WriteLine($"  Top Category: {month.Categories}");
            }


            var futureProjections = categories
                .Select(c => new
                {
                    c.Name,
                    RecurringExpenses = expenses
                        .Where(e => e.Category == c.Name && e.IsRecurring)
                        .Average(e => e.Amount),
                    DaysUntilMonthEnd = DateTime.DaysInMonth(currentYear, currentMonth) - DateTime.Now.Day
                })
                .Where(x => x.RecurringExpenses > 0)
                .Select(x => new
                {
                    x.Name,
                    ProjectedAmount = Math.Round(x.RecurringExpenses * 2), // Assuming 2 more recurring payments
                    Confidence = "HIGH" // Because they're recurring
                })
                .ToList();

            Console.WriteLine("\n=== Future Expense Projections ===");
            foreach (var projection in futureProjections)
            {
                Console.WriteLine($"{projection.Name}:");
                Console.WriteLine($"  Projected: {projection.ProjectedAmount:C0}");
                Console.WriteLine($"  Confidence: {projection.Confidence}");
            }

            var currentMonthIncome = incomes
                .Where(i => i.Date.Month == currentMonth && i.Date.Year == currentYear)
                .Sum(i => i.Amount);

            var currentMonthTotalExpenses = expenses
                .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear)
                .Sum(e => e.Amount);

            var savings = currentMonthIncome - currentMonthTotalExpenses;
            var savingsRate = currentMonthIncome > 0 ?
                Math.Round(savings / currentMonthIncome * 100, 1) : 0;

            Console.WriteLine("\n=== Income vs Expenses (Current Month) ===");
            Console.WriteLine($"Total Income: {currentMonthIncome:C0}");
            Console.WriteLine($"Total Expenses: {currentMonthTotalExpenses:C0}");
            Console.WriteLine($"Savings: {savings:C0} ({savingsRate}%)");

            if (savings > 0)
            {
                Console.WriteLine("✅ You're saving money this month!");
            }
            else if (savings < 0)
            {
                Console.WriteLine("⚠️ You're spending more than you earn!");
            }
            else
            {
                Console.WriteLine("⚖️ You're breaking even this month.");
            }

            var paymentMethods = expenses
                .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear)
                .GroupBy(e => e.PaymentMethod)
                .Select(g => new
                {
                    Method = g.Key,
                    TotalAmount = g.Sum(e => e.Amount),
                    Count = g.Count(),
                    Percentage = Math.Round((double)g.Sum(e => e.Amount) / currentMonthTotalExpenses * 100, 1)
                })
                .OrderByDescending(p => p.TotalAmount)
                .ToList();

            Console.WriteLine("\n=== Payment Methods Usage ===");
            foreach (var method in paymentMethods)
            {
                Console.WriteLine($"{method.Method}:");
                Console.WriteLine($"  Amount: {method.TotalAmount:C0} ({method.Percentage}%)");
                Console.WriteLine($"  Transactions: {method.Count}");
            }

        }
    }
}
