using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
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
using ParkingRight.WebApi.Filters;
using ParkingRight.WebApi.SNS;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ParkingRight API", Version = "v1"});
            });
            
            #region AWS
            var awsOptions = Configuration.GetAWSOptions();
            awsOptions.Region = RegionEndpoint.EUCentral1;
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddAWSService<IAmazonSimpleNotificationService>();
            services.AddDefaultAWSOptions(awsOptions);
            services.AddScoped<IDynamoDBContext>(provider => new DynamoDBContext(provider.GetService<IAmazonDynamoDB>()));

            #endregion


            services.RegisterDomainModule();
            services.RegisterDataAccessModule();
                
            RegisterSubmodules(services);
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddAutoMapper(typeof(ParkingRightProfile));
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            var swaggerUrl = "/Prod/swagger/v1/swagger.json";

#if DEBUG
            swaggerUrl = "/swagger/v1/swagger.json";
#endif
            app.UseSwaggerUI(c => { c.SwaggerEndpoint(swaggerUrl, "ParkingRight API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync("Welcome to ParkingRightWebApi ASP.NET Core on AWS Lambda");
                    });
            });
        }
        
    }
}