using System;
using CustomerStorageTestWeb.Services;
using CustomerStorageTestWeb.Services.Abstractions;
using CustomerStorageTestWeb.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerStorageTestWeb
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });
            
            var customerStorageSettings = _configuration.GetSection(CustomerStorageSettings.SectionName).Get<CustomerStorageSettings>();

            switch (customerStorageSettings.StorageType)
            {
                case StorageTypeEnum.File:
                    services.AddScoped<ICustomersRepository, FileCustomerStorage>();
                    break;
                case StorageTypeEnum.Cache:
                    services.AddScoped<ICustomersRepository, CacheCustomerStorage>();
                    services.AddMemoryCache(_ => new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(300))
                        .SetPriority(CacheItemPriority.Normal));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}