using System;
using System.Collections.Generic;
using System.Text;

namespace Gateways.Business.Models
{
    public class ResponseModel
    {
        public int TotalCount { get; set; }

        public ICollection<GatewayModel> Gateways { get; set; }
    }
}
