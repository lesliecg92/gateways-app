using System.Linq;
using FluentValidation;
using Gateways.Business;
using Gateways.Business.Models;

namespace GatewaysApp.Validators
{
    public class GatewayValidator : AbstractValidator<GatewayModel>
    {
        public GatewayValidator(IGatewayService gatewayService)
        {
            RuleFor(g => g.IpAddress)
                .NotEmpty().WithMessage("Gateway ip address is required")
                .Must(
                ipString =>
                {
                    if (string.IsNullOrWhiteSpace(ipString))
                    {
                        return false;
                    }

                    var splitValues = ipString.Split('.');
                    if (splitValues.Length != 4)
                    {
                        return false;
                    }

                    byte tempForParsing;

                    return splitValues.All(r => byte.TryParse(r, out tempForParsing));
                })
                .WithMessage("You must enter a valid IPv4 Address");
            RuleFor(g => g.Name).NotEmpty().WithMessage("Gateway name is required");

            RuleFor(g => g.SerialNumber).NotEmpty().WithMessage("Gateway serial number is required");
            RuleFor(g => g)
                .MustAsync(async (g, cancellation) => await gatewayService.IsGatewayNameNonUnique(g))
                .WithMessage("Gateway name must be unique");

        }
    }
}
