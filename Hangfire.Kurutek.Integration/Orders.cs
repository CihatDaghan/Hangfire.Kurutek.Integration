namespace Hangfire.Kurutek.Integration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public string orderNumber { get; set; }

        public double grossAmount { get; set; }

        public double totalDiscount { get; set; }

        public double totalPrice { get; set; }

        public string customerFirstName { get; set; }

        public string customerEmail { get; set; }

        public int customerId { get; set; }

        public string customerLastName { get; set; }

        public long cargoTrackingNumber { get; set; }

        public string cargoTrackingLink { get; set; }

        public string cargoSenderNumber { get; set; }

        public string cargoProviderName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime orderDate { get; set; }

        public string tcIdentityNumber { get; set; }

        public string currencyCode { get; set; }

        public string shipmentPackageStatus { get; set; }

        public long estimatedDeliveryStartDate { get; set; }

        public long estimatedDeliveryEndDate { get; set; }

        public string deliveryAddressType { get; set; }

        public long agreedDeliveryDate { get; set; }

        public int marketplace { get; set; }
    }
}
