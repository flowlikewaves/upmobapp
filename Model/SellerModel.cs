using System;
using System.Collections.Generic;

namespace Mobappg4v2.Model
{
    public class SellerModel
    {
        public string SellerId { get; set; }
        public string StoreName { get; set; }
        public string SellerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string StoreDescription { get; set; }
        public DateTime JoinDate { get; set; }
        public string ProfileImage { get; set; }
        public double Rating { get; set; }
        public int TotalSales { get; set; }
        public bool IsVerified { get; set; }
    }
} 