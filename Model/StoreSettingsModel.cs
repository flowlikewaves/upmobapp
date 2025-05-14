using System;
using System.Collections.Generic;

namespace Mobappg4v2.Model
{
    public class StoreSettingsModel
    {
        // Store Profile
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string BannerUrl { get; set; }
        public List<string> StoreImages { get; set; }
        public bool IsVerified { get; set; }

        // Business Information
        public string BusinessType { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        // Payment Settings
        public List<string> AcceptedPaymentMethods { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string SwiftCode { get; set; }

        // Shipping Settings
        public bool OffersLocalDelivery { get; set; }
        public bool OffersInternationalShipping { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal FreeShippingThreshold { get; set; }
        public List<ShippingZone> ShippingZones { get; set; }

        // Notification Preferences
        public bool EmailNotifications { get; set; }
        public bool PushNotifications { get; set; }
        public bool OrderNotifications { get; set; }
        public bool MessageNotifications { get; set; }
        public bool PromotionalNotifications { get; set; }

        public StoreSettingsModel()
        {
            StoreImages = new List<string>();
            AcceptedPaymentMethods = new List<string>();
            ShippingZones = new List<ShippingZone>();
        }
    }

    public class ShippingZone
    {
        public string ZoneName { get; set; }
        public List<string> Countries { get; set; }
        public decimal BaseRate { get; set; }
        public decimal AdditionalItemRate { get; set; }
        public int EstimatedDays { get; set; }

        public ShippingZone()
        {
            Countries = new List<string>();
        }
    }
} 