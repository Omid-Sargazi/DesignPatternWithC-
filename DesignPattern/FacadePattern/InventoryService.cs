using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.FacadePattern
{
    public class InventoryService
    {
        public bool CheckStock(string productId)
        {
            Console.WriteLine("✅ Checking stock for product: " + productId);
            return true;
        }
    }

    public class ShippingService
    {
        public void ShipTo(string address)
        {
            Console.WriteLine("🚚 Shipping to: " + address);
        }
    }

    public class PaymentService
    {
        public void pay(string account, double amount)
        {
            Console.WriteLine($"💳 Paying {amount} from account {account}");
        }
    }

    public class OrderFacade
    {
        private readonly InventoryService _inventoryService;
        private readonly ShippingService _shippingService;
        private readonly PaymentService _paymentService;
        public OrderFacade(InventoryService inventoryService,ShippingService shippingService, PaymentService paymentService)
        {
            _inventoryService = inventoryService;
            _shippingService = shippingService;
            _paymentService = paymentService;
        }

        public void PlaceOrder(string productId, string customerAddress, string account, double amount)
        {
            if (_inventoryService.CheckStock(productId))
            {
                _paymentService.pay(account, amount);
                _shippingService.ShipTo(customerAddress);
                Console.WriteLine("🎉 Order placed successfully!");
            }
            else
            {
                Console.WriteLine("❌ Product is out of stock.");
            }
        }
    }
}
