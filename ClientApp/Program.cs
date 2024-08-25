using Client.Services;
using ClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5264/api/")
            });
            builder.Services.AddScoped<IInsuranceTypeDtoService, InsuranceTypeDtoService>();
            builder.Services.AddScoped<IPolicyHolderDtoService, PolicyHolderDtoService>();
            builder.Services.AddScoped<IHospitalDtoService, HospitalDtoService>();
            builder.Services.AddScoped<IClaimDtoService, ClaimDtoService>();
            builder.Services.AddScoped<IInsuredPolicyDtoService, InsuredPolicyDtoService>();
            builder.Services.AddScoped<IInsuredDtoService, InsuredDtoService>();
            builder.Services.AddScoped<IPolicyDtoService, PolicyDtoService>(); 


            await builder.Build().RunAsync();
        }
    }
}
