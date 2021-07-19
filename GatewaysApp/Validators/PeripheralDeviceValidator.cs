using FluentValidation;
using Gateways.Business;
using Gateways.Business.Models;

namespace GatewaysApp.Validators
{
    public class PeripheralDeviceValidator : AbstractValidator<PeripheralDeviceModel>
    {
        public PeripheralDeviceValidator(IGatewayService gatewayService)
        {
            RuleFor(d => d.DateCreated).NotEmpty().WithMessage("Date created is required");
            RuleFor(d => d.Status).IsInEnum().WithMessage("Wrong status code");
            RuleFor(d => d.UID).NotEmpty().WithMessage("UID is required");
            RuleFor(d => d.Vendor).NotEmpty().WithMessage("Vendor is required");
            //RuleFor(d => d).MustAsync(async (g, cancellation) => await gatewayService.GatewayAcceptsDevice(g))
        }
    }
}
