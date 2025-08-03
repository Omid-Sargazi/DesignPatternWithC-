using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ObserverPattern
{
    public enum NotificationType
    {
        PriceChange,
        VolumeChange,
        SignificantMovement,
        NewsAlert,
    }
    public class StockUpdate
    {
        public string Symbol { get; }
        public decimal Price { get; }
        public decimal ChangePercent { get; }
        public long Volume {  get; }
        public DateTime UpdateTime { get; }

        public StockUpdate(string symbol, decimal price, decimal changePercent, long volume)
        {
            Symbol = symbol;
            Price = price;
            ChangePercent = changePercent;
            Volume = volume;
            UpdateTime = DateTime.Now;
        }
    }

    public interface IStockObserver
    {
        string Name { get; }
        Dictionary<string,(decimal minChange, NotificationType typr)> Subscriptions { get; }
        void Update(StockUpdate update, NotificationType notificationType);
    }

}
