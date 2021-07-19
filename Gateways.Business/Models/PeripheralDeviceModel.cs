using System;
using Gateways.Repository.Domain;

namespace Gateways.Business.Models
{
    public class PeripheralDeviceModel
    {
        public int Id { get; set; }

        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public Status Status { get; set; }
    }
}
