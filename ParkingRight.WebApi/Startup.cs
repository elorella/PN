using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ParkingRight.DataAccess;
using ParkingRight.Domain;
using ParkingRight.Domain.Profile;

namespace ParkingRight.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ParkingRight API", Version = "v1"});
            });

            services.AddAWSService<IAmazonDynamoDB>();
            services.AddDefaultAWSOptions(
                new AWSOptions
                {
                    Region = RegionEndpoint.GetBySystemName("us-west-2")
                });
            RegisterSubmodules(services);
            services.AddAutoMapper(typeof(ParkingRightProfile));

            services.AddHttpClient<IPrdbIntegrationProcessor, PrdbIntegrationProcessor>(c =>
                c.BaseAddress = new Uri(Configuration["ApiConfigs:PrdbIntegration:Uri"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "prod";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParkingRight API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                    });
            });
        }

        protected virtual void RegisterSubmodules(IServiceCollection services)
        {
            DomainModule.RegisterTo(services);
            DataAccessModule.RegisterTo(services);
        }
    }
}