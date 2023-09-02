using _2.Web.Gateway.Application.Interfaces.Urls;
using _2.Web.Gateway.Application.Interfaces.Users.Auth;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Users.Auth;
using _3.Web.Gateway.Infrastructure__EFCore_.Transients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace _4.Web.Gateway.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "_4.Web.Gateway.API", Version = "v1" });
            });

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IUrlsService, UrlsService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IServiceUnit, ServiceUnit>();
            services.AddTransient<IHandlerUnit, HandlerUnit>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins(
                    "http://localhost:19006/",
                    "http://10.240.90.18:19006/");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
            });

            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_4.Web.Gateway.API v1"));
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
