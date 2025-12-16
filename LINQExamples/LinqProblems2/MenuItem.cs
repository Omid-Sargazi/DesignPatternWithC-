using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.LinqProblems2
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // "Appetizer", "Main Course", "Dessert", "Drink"
        public decimal Price { get; set; }
        public int PreparationTime { get; set; } // in minutes
        public bool IsAvailable { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime FirstVisit { get; set; }
        public int TotalVisits { get; set; }
    }

    public class RestaurantTable
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsOccupied { get; set; }
        public string Location { get; set; } // "Indoor", "Outdoor", "VIP"
    }

    public class Order
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? ServeTime { get; set; }
        public DateTime? PaymentTime { get; set; }
        public string Status { get; set; } // "Pending", "Preparing", "Served", "Paid"
        public decimal TotalAmount { get; set; }
        public int NumberOfGuests { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; }
    }

    public class RestaurantManagement
    {
        public static void Run()
        {
            var menuItems = new List<MenuItem>
        {
            new MenuItem { Id = 1, Name = "Caesar Salad", Category = "Appetizer", Price = 120000, PreparationTime = 10, IsAvailable = true },
            new MenuItem { Id = 2, Name = "Garlic Bread", Category = "Appetizer", Price = 80000, PreparationTime = 5, IsAvailable = true },
            new MenuItem { Id = 3, Name = "Grilled Chicken", Category = "Main Course", Price = 250000, PreparationTime = 20, IsAvailable = true },
            new MenuItem { Id = 4, Name = "Beef Steak", Category = "Main Course", Price = 350000, PreparationTime = 25, IsAvailable = true },
            new MenuItem { Id = 5, Name = "Vegetable Pizza", Category = "Main Course", Price = 200000, PreparationTime = 15, IsAvailable = true },
            new MenuItem { Id = 6, Name = "Chocolate Cake", Category = "Dessert", Price = 90000, PreparationTime = 5, IsAvailable = true },
            new MenuItem { Id = 7, Name = "Ice Cream", Category = "Dessert", Price = 60000, PreparationTime = 2, IsAvailable = false }, // اتمام موجودی
            new MenuItem { Id = 8, Name = "Orange Juice", Category = "Drink", Price = 50000, PreparationTime = 2, IsAvailable = true },
            new MenuItem { Id = 9, Name = "Coffee", Category = "Drink", Price = 40000, PreparationTime = 3, IsAvailable = true }
        };

            // مشتریان
            var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Ali Rezaei", Phone = "09123456789", FirstVisit = DateTime.Now.AddDays(-60), TotalVisits = 5 },
            new Customer { Id = 2, Name = "Sara Mohammadi", Phone = "09129876543", FirstVisit = DateTime.Now.AddDays(-30), TotalVisits = 3 },
            new Customer { Id = 3, Name = "Reza Ahmadi", Phone = "09121122334", FirstVisit = DateTime.Now.AddDays(-90), TotalVisits = 8 },
            new Customer { Id = 4, Name = "Maryam Hosseini", Phone = "09123344556", FirstVisit = DateTime.Now.AddDays(-15), TotalVisits = 1 }
        };

            // میزهای رستوران
            var tables = new List<RestaurantTable>
        {
            new RestaurantTable { Id = 1, TableNumber = "A1", Capacity = 2, IsOccupied = false, Location = "Indoor" },
            new RestaurantTable { Id = 2, TableNumber = "A2", Capacity = 4, IsOccupied = true, Location = "Indoor" },
            new RestaurantTable { Id = 3, TableNumber = "A3", Capacity = 6, IsOccupied = false, Location = "Indoor" },
            new RestaurantTable { Id = 4, TableNumber = "B1", Capacity = 2, IsOccupied = true, Location = "Outdoor" },
            new RestaurantTable { Id = 5, TableNumber = "B2", Capacity = 4, IsOccupied = false, Location = "Outdoor" },
            new RestaurantTable { Id = 6, TableNumber = "VIP1", Capacity = 8, IsOccupied = false, Location = "VIP" }
        };

            // سفارشات
            var orders = new List<Order>
        {
            new Order { Id = 1, TableId = 2, CustomerId = 1, OrderTime = DateTime.Now.AddMinutes(-45),
                       ServeTime = DateTime.Now.AddMinutes(-20), PaymentTime = DateTime.Now.AddMinutes(-15),
                       Status = "Paid", TotalAmount = 420000, NumberOfGuests = 3 },
            new Order { Id = 2, TableId = 4, CustomerId = 2, OrderTime = DateTime.Now.AddMinutes(-30),
                       ServeTime = DateTime.Now.AddMinutes(-10), PaymentTime = null,
                       Status = "Served", TotalAmount = 310000, NumberOfGuests = 2 },
            new Order { Id = 3, TableId = 6, CustomerId = 3, OrderTime = DateTime.Now.AddMinutes(-15),
                       ServeTime = null, PaymentTime = null,
                       Status = "Preparing", TotalAmount = 890000, NumberOfGuests = 6 },
            new Order { Id = 4, TableId = 1, CustomerId = 4, OrderTime = DateTime.Now.AddMinutes(-5),
                       ServeTime = null, PaymentTime = null,
                       Status = "Pending", TotalAmount = 170000, NumberOfGuests = 2 }
        };

            // آیتم‌های سفارش
            var orderItems = new List<OrderItem>
        {
            // Order 1
            new OrderItem { Id = 1, OrderId = 1, MenuItemId = 1, Quantity = 1, SpecialInstructions = "No cheese" },
            new OrderItem { Id = 2, OrderId = 1, MenuItemId = 3, Quantity = 2, SpecialInstructions = "Well done" },
            new OrderItem { Id = 3, OrderId = 1, MenuItemId = 8, Quantity = 3, SpecialInstructions = "" },
            
            // Order 2
            new OrderItem { Id = 4, OrderId = 2, MenuItemId = 2, Quantity = 1, SpecialInstructions = "Extra garlic" },
            new OrderItem { Id = 5, OrderId = 2, MenuItemId = 5, Quantity = 1, SpecialInstructions = "" },
            new OrderItem { Id = 6, OrderId = 2, MenuItemId = 9, Quantity = 2, SpecialInstructions = "" },
            
            // Order 3
            new OrderItem { Id = 7, OrderId = 3, MenuItemId = 1, Quantity = 2, SpecialInstructions = "" },
            new OrderItem { Id = 8, OrderId = 3, MenuItemId = 4, Quantity = 4, SpecialInstructions = "Medium rare" },
            new OrderItem { Id = 9, OrderId = 3, MenuItemId = 6, Quantity = 3, SpecialInstructions = "" },
            new OrderItem { Id = 10, OrderId = 3, MenuItemId = 8, Quantity = 6, SpecialInstructions = "" },
            
            // Order 4
            new OrderItem { Id = 11, OrderId = 4, MenuItemId = 5, Quantity = 1, SpecialInstructions = "No mushrooms" },
            new OrderItem { Id = 12, OrderId = 4, MenuItemId = 9, Quantity = 2, SpecialInstructions = "" }
        };
        }

    }

}
