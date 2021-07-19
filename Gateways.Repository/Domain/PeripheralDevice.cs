using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gateways.Repository.Domain
{
    public class PeripheralDevice
    {
        [Key]
        public int Id { get; set; }

        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public Status Status { get; set; }

        public Gateway Gateway { get; set; }

        [ForeignKey("Gateway")]
        public int GatewayId { get; set; }
    }

    public enum Status
    {
        Online,
        Offline
    }
}
