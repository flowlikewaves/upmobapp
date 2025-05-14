using System;
using System.Collections.Generic;

namespace Mobappg4v2.Model
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public List<string> Images { get; set; }
        public bool IsAvailable { get; set; }
        public string SellerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<string> Tags { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string SKU { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public Dictionary<string, string> Specifications { get; set; }

        public ProductModel()
        {
            Images = new List<string>();
            Tags = new List<string>();
            Specifications = new Dictionary<string, string>();
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            IsAvailable = true;
        }
    }
} 