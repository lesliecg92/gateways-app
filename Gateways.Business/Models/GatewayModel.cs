using System.Collections.Generic;

namespace Gateways.Business.Models
{
    public class GatewayModel
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public ICollection<PeripheralDeviceModel> PeripheralDevices { get; set; }
    }
}
