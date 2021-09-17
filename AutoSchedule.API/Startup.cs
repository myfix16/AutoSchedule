using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AutoSchedule.API
{
    public class Startup
    {
        internal const string CorsAllowSpecificOrigins = "AllowSpecificOrigins";
        internal const string CorsAllowAll = "AllowAllOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoSchedule.API", Version = "v1" });
            });
            // Add Cors policy
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsAllowSpecificOrigins, policy =>
                    policy.WithOrigins("http://autoschedule.azurewebsites.net",
                                       "https://autoschedule.azurewebsites.net",
                                       "http://localhost:44329",
                                       "https://localhost:44329")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

                // options.AddPolicy(name: CorsAllowAll, policy =>
                //     policy.AllowAnyOrigin()
                //         .AllowAnyHeader()
                //         .AllowAnyMethod()
                //         .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoSchedule.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
