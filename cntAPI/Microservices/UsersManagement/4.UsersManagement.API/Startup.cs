using _2.UsersManagement.Application.Interfaces;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;
using _3.UsersManagement.Infrastructure__EFCore_.Transients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace _4.UsersManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "_4.UsersManagement.API", Version = "v1" });
            });

            services.AddDbContext<CNTContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<CredinetContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection2"));
            });
            
            services.AddTransient<IGenericUnit, GenericUnit>();
            services.AddTransient<IServiceUnit, ServiceUnit>();
            services.AddTransient<IHandlerUnit, HandlerUnit>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_4.UsersManagement.API v1"));
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
