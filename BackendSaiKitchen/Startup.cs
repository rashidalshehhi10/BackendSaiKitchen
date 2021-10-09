using Azure.Storage.Blobs;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using BackendSaiKitchen.Models;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Sentry.AspNetCore;
using Serilog.Context;
using Stripe;
using System;

namespace BackendSaiKitchen
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sai Kitchen Documentation", Version = "v1" });

            });
            services.AddDbContextPool<BackendSaiKitchen_dbContext>(options => options.UseSqlServer("Server=backendsaikitchendbserver.database.windows.net,1433;Database=BackendSaiKitchen_db;User Id=SaiAdmin;Password=SaiKitchen123;MultipleActiveResultSets=true;"));


            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:8080/", "http://localhost:3000", "http://localhost:3000/", "http://localhost:8080", "https://saikitchen.azurewebsites.net/", "https://saikitchen.azurewebsites.net", "http://saikitchen.azurewebsites.net/", "http://saikitchen.azurewebsites.net", "*")
                .AllowAnyHeader()
                .AllowAnyMethod());
            });


            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            });
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();


            services.AddScoped(x =>
            new BlobServiceClient(Configuration.GetConnectionString("AzureStorge")));

            services.AddScoped<IBlobManager, BlobManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = "sk_test_51JA8pgAtqGclDTLoAv6UT77NclL3nUSuGYSuK1tIM0SfQKOfx7I3hj6offRjpkw9sSztIbQE6OnGOQNBFVYkvlSQ00H2xue74N";

            app.UseDeveloperExceptionPage();
            

            app.UseSwagger();
           

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sai Kitchen v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsApi");


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSentryTracing();
            try
            {
                // below code is needed to get User name for Log             
                app.Use(async (httpContext, next) =>
                {
                    var userName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Guest"; 
                    LogContext.PushProperty("Username", userName); 
                    await next.Invoke();
                }
                );
            }
            catch (Exception e)
            {
                Sentry.SentrySdk.CaptureMessage(e.Message);
            }

        }
    }
}
