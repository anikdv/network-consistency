using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OperatorWebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var clientUri = new Uri("http://localhost:5100");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped(_ => new HttpClient {BaseAddress = clientUri});

            await builder.Build().RunAsync();
        }
    }
}