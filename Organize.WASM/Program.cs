using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Organize.Shared.Contracts;
using Organize.Business;
using Organize.DataAccess;
using Organize.Shared.Entities;
using Organize.TestFake;
using Organize.WASM.ItemEdit;


namespace Organize.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //builder.Services.AddSingleton<IUserManager, UserManager>();
            builder.Services.AddScoped<IUserManager, UserManagerFake>();
            builder.Services.AddScoped<IUserItemManager, UserItemManager>();
            builder.Services.AddScoped<IItemDataAccess, ItemDataAccess>();
            builder.Services.AddScoped<IPersistanceService,InMemoryStorage.InMemoryStorage>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<ItemEditService>();
          //  await builder.Build().RunAsync();

            var host = builder.Build();
            var persistanceService = host.Services.GetRequiredService<IPersistanceService>();
            await persistanceService.InitAsync();

            var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();
            var userItemManager = host.Services.GetRequiredService<IUserItemManager>();
            var userManager = host.Services.GetRequiredService<IUserManager>();

            if (persistanceService is InMemoryStorage.InMemoryStorage)
            {
                TestData.CreateTestUser(userItemManager, userManager);
                currentUserService.CurrentUser = TestData.TestUser;
            }


          

            
            await host.RunAsync();
        }
    }
}
