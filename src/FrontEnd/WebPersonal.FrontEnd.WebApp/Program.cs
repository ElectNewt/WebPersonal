using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebPersonal.FrontEnd.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("BackendApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44363");
            }
                );

            builder.Services.AddSingleton<StateContainer>();

            await builder.Build().RunAsync();
        }
    }
}
