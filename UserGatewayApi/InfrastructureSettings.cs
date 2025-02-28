
using UserGatewayApi.Options;
using UserGatewayApi.Services;

namespace UserGatewayApi
{
    public class InfrastructureSettings
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IGatewayService, GatewayService>();
            services.AddOptions<GatewayServiceOptions>().BindConfiguration("GatewayServiceOptions");
        }
    }
}
