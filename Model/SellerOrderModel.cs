using System;
using System.Collections.Generic;

namespace Mobappg4v2.Model
{
    public class SellerOrderModel
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public List<OrderItemModel> Items { get; set; }
        public string Notes { get; set; }

        public SellerOrderModel()
        {
            Items = new List<OrderItemModel>();
            OrderDate = DateTime.Now;
        }
    }

    public class OrderItemModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;
        public string SKU { get; set; }
    }
} 