using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;
using NodaTime.Extensions;
using StarkAlpine.LiftStatus.Api.Profiles;
using StarkAlpine.LiftStatus.Business;
using StarkAlpine.LiftStatus.Business.Configuration;

namespace StarkAlpine.LiftStatus.Api
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

            services
                .AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "StarkAlpine.LiftStatus", Version = "v1"});
            });

            services.AddAutoMapper(typeof(LiftProfile));

            var resortOptions = new ResortOptions();
            Configuration.GetSection(nameof(ResortOptions)).Bind(resortOptions);

            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddSingleton(resortOptions);
            services.AddSingleton<ILiftStatusService, LiftStatusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StarkAlpine.LiftStatus v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
