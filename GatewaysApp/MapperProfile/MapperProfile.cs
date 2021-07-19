using AutoMapper;
using Gateways.Business.Models;
using Gateways.Repository.Domain;

namespace GatewaysApp.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GatewayModel, Gateway>()
                .ReverseMap();
            CreateMap<PeripheralDeviceModel, PeripheralDevice>()
                .ReverseMap();
        }
    }
}
