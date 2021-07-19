using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gateways.Repository.Domain
{
    public class Gateway
    {
        [Key]
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public ICollection<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
