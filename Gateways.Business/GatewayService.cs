using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gateways.Business.Models;
using Gateways.Repository.Domain;
using Gateways.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Business
{
    public class GatewayService : IGatewayService
    {
        private readonly IRepository<Gateway> _gatewayRepository;
        private readonly IRepository<PeripheralDevice> _peripheralDeviceRepository;
        private readonly IMapper _mapper;

        public GatewayService(IRepository<Gateway> gatewayRepository, IRepository<PeripheralDevice> peripheralDeviceRepository, IMapper mapper)
        {
            _gatewayRepository = gatewayRepository;
            _peripheralDeviceRepository = peripheralDeviceRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetGateways(Pagination pagination)
        {
            var query = _gatewayRepository.FindAll();

            var responseModel = new ResponseModel
            {
                TotalCount = query.Count()
            };

            query = query.Skip(pagination.PageSize * (pagination.Page - 1)).Take(pagination.PageSize)
                .Include(g => g.PeripheralDevices);
            responseModel.Gateways = await _mapper.ProjectTo<GatewayModel>(query).ToListAsync();
            return responseModel;
        }

        public async Task<GatewayModel> GetGateway(int id)
        {
            var gateway = await _gatewayRepository.FindById(id, null, new []{"PeripheralDevices"});
            return _mapper.Map<GatewayModel>(gateway);
        }

        public async Task<GatewayModel> AddGateway(GatewayModel gatewayModel)
        {
            var gateway = _mapper.Map<Gateway>(gatewayModel);
            var inserted = await _gatewayRepository.InsertAsync(gateway);
            return _mapper.Map<GatewayModel>(inserted);
        }

        public async Task<GatewayModel> UpdateGateway(GatewayModel gatewayModel)
        {
            var gateway = await _gatewayRepository.FindById(gatewayModel.Id);
            _mapper.Map(gatewayModel, gateway);
            var updated = _gatewayRepository.Update(gateway);
            return _mapper.Map<GatewayModel>(updated);
        }

        public async Task<bool> RemoveGateway(int id)
        {
            await _gatewayRepository.RemoveAsync(id);
            return true;
        }

        public async Task<ICollection<PeripheralDeviceModel>> GetDevices(int gatewayId)
        {
            var query = _peripheralDeviceRepository.Find(p => p.GatewayId == gatewayId);
            return await _mapper.ProjectTo<PeripheralDeviceModel>(query).ToListAsync();
        }

        public async Task<PeripheralDeviceModel> GetDevice(int deviceId)
        {
            var device = await _peripheralDeviceRepository.FindById(deviceId);
            return _mapper.Map<PeripheralDeviceModel>(device);
        }

        public async Task<PeripheralDeviceModel> AddDevice(int gatewayId, PeripheralDeviceModel peripheralDeviceModel)
        {
            var device = _mapper.Map<PeripheralDevice>(peripheralDeviceModel);
            device.GatewayId = gatewayId;
            var inserted = await _peripheralDeviceRepository.InsertAsync(device);
            return _mapper.Map<PeripheralDeviceModel>(inserted);
        }

        public async Task<PeripheralDeviceModel> UpdateDevice(PeripheralDeviceModel peripheralDeviceModel)
        {
            var device = await _peripheralDeviceRepository.FindFirstOrDefaultAsync(d => d.Id == peripheralDeviceModel.Id);
            _mapper.Map(peripheralDeviceModel, device);
            var updated = _peripheralDeviceRepository.Update(device);
            return _mapper.Map<PeripheralDeviceModel>(updated);
        }

        public async Task<bool> RemoveDevice(int id)
        {
            await _peripheralDeviceRepository.RemoveAsync(id);
            return true;
        }

        public async Task<bool> GatewayAcceptsDevice(int gatewayId)
        {
            var gateway = await _gatewayRepository.FindById(gatewayId, null, new[] {"PeripheralDevices"});

            return gateway.PeripheralDevices == null || gateway.PeripheralDevices.Count < 10;
        }

        public async Task<bool> IsGatewayNameNonUnique(GatewayModel model)
        {
            var gateway = await _gatewayRepository.FindFirstOrDefaultAsync(g => g.Id == model.Id);
            bool result;
            if (gateway == null)
            {
                result = await _gatewayRepository.FindAll().AnyAsync(g => g.Name == model.Name);
            }
            else
            {
                result = await _gatewayRepository.FindAll().AnyAsync(g => g.Name == model.Name && g.Id == model.Id);
            }
            return !result;
        }
    }
}
