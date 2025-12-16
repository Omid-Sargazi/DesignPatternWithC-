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

}
