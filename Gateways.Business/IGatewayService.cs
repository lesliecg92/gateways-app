using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gateways.Business.Models;

namespace Gateways.Business
{
    public interface IGatewayService
    {
        public Task<ResponseModel> GetGateways(Pagination pagination);
        public Task<GatewayModel> GetGateway(int id);
        public Task<GatewayModel> AddGateway(GatewayModel gatewayModel);
        public Task<GatewayModel> UpdateGateway(GatewayModel gatewayModel);
        public Task<bool> RemoveGateway(int id);
        public Task<ICollection<PeripheralDeviceModel>> GetDevices(int gatewayId);
        public Task<PeripheralDeviceModel> GetDevice(int deviceId);
        public Task<PeripheralDeviceModel> AddDevice(int gatewayId, PeripheralDeviceModel peripheralDeviceModel);
        public Task<PeripheralDeviceModel> UpdateDevice(PeripheralDeviceModel peripheralDeviceModel);
        public Task<bool> RemoveDevice(int id);
        public Task<bool> GatewayAcceptsDevice(int gatewayId);
        public Task<bool> IsGatewayNameNonUnique(GatewayModel model);
    }
}
