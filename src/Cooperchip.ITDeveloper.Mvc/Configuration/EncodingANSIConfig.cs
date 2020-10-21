using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class EncodingANSIConfig
    {
        public static IServiceCollection AddProviderPageCode(this IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return services;
        }
    }
}
