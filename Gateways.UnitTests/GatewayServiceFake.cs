using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gateways.Business;
using Gateways.Business.Models;
using Gateways.Repository.Domain;

namespace Gateways.UnitTests
{
    public class GatewayServiceFake : IGatewayService
    {
        private List<GatewayModel> list;

        public GatewayServiceFake()
        {
            list = new List<GatewayModel>
            {
                new GatewayModel
                {
                    Id = 1,
                    IpAddress = "10.10.10.10",
                    Name = "Gateway 1",
                    SerialNumber = "QEW23FR",
                    PeripheralDevices = new List<PeripheralDeviceModel>
                    {
                        new PeripheralDeviceModel
                        {
                            Id = 1,
                            DateCreated = DateTime.Now,
                            Status = Status.Offline,
                            UID = 573,
                            Vendor = "Vendor 1"
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 2,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 2",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 3,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 3",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 4,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 4",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 5,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 5",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 6,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 6",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 7,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 7",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 8,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 8",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 9,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 9",
                            Status = Status.Online,
                            UID = 6933
                        },
                        new PeripheralDeviceModel
                        {
                            Id = 10,
                            DateCreated = DateTime.Now.AddDays(-10),
                            Vendor = "Vendor 10",
                            Status = Status.Online,
                            UID = 6933
                        }
                    }
                },
                new GatewayModel
                {
                    Name = "Gateway 2",
                    IpAddress = "39.39.39.39",
                    SerialNumber = "34MJF",
                    PeripheralDevices = null,
                    Id = 2
                }
            };
        }

        public Task<ResponseModel> GetGateways(Pagination pagination)
        {
            return Task.FromResult(new ResponseModel
            {
                Gateways = list,
                TotalCount = list.Count
            });
        }

        public Task<GatewayModel> GetGateway(int id)
        {
            return Task.FromResult(list.FirstOrDefault(l => l.Id == id));
        }

        public Task<GatewayModel> AddGateway(GatewayModel gatewayModel)
        {
            var last = list.OrderBy(l => l.Id).LastOrDefault();
            var id = last == null ? 0 : last.Id + 1;

            gatewayModel.Id = id;

            list.Add(gatewayModel);

            return Task.FromResult(gatewayModel);
        }

        public Task<GatewayModel> UpdateGateway(GatewayModel gatewayModel)
        {
            var gateway = list.FirstOrDefault(l => l.Id == gatewayModel.Id);
            gateway.IpAddress = gatewayModel.IpAddress;
            gateway.Name = gatewayModel.Name;
            gateway.SerialNumber = gatewayModel.SerialNumber;

            return Task.FromResult(gatewayModel);
        }

        public Task<bool> RemoveGateway(int id)
        {
            list.Remove(list.First(l => l.Id == id));
            return Task.FromResult(true);
        }

        public Task<ICollection<PeripheralDeviceModel>> GetDevices(int gatewayId)
        {
            throw new NotImplementedException();
        }

        public Task<PeripheralDeviceModel> GetDevice(int deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<PeripheralDeviceModel> AddDevice(int gatewayId, PeripheralDeviceModel peripheralDeviceModel)
        {
            var gateway = list.Find(item => item.Id == gatewayId);
            if (gateway == null)
            {
                return null;
            }

            gateway.PeripheralDevices ??= new List<PeripheralDeviceModel>();

            gateway.PeripheralDevices.Add(peripheralDeviceModel);

            return Task.FromResult(peripheralDeviceModel);
        }

        public Task<PeripheralDeviceModel> UpdateDevice(PeripheralDeviceModel peripheralDeviceModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveDevice(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GatewayAcceptsDevice(int gatewayId)
        {
            var gateway = list.Find(item => item.Id == gatewayId);
            if (gateway == null)
            {
                return Task.FromResult(false);
            }

            if (gateway.PeripheralDevices == null)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(gateway.PeripheralDevices.Count < 10);
        }

        public Task<bool> IsGatewayNameNonUnique(GatewayModel model)
        {
            throw new NotImplementedException();
        }
    }
}
