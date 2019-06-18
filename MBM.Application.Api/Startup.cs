using System;
using System.IO;
using System.Reflection;
using MBM.Application.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MBM.Application.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// The configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services. (註冊服務)
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            // Swagger 產生器是負責取得 API 的規格並產生 SwaggerDocument 物件。
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "API",
                        Description = "Test API with ASP.NET Core 3.0",
                        Contact = new OpenApiContact()
                        {
                            Name = "Albert",
                            Email = "albert.shen@gib4u.com",
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the specified application. (註冊 Middleware)
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios,
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // 優先註冊 ExceptionMiddleware，
            // 如此後面註冊的 Middleware 出錯時皆可捕獲
            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger Middleware 負責路由，提供 SwaggerDocument 物件。
            app.UseSwagger();

            // SwaggerUI 是負責將 SwaggerDocument 物件變成漂亮的介面。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    //// url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    //// name: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "Test API V1");

                // 若要在應用程式的根目錄(http://localhost:<port>/) 上提供 Swagger UI，請將 RoutePrefix 屬性設為空字串
                c.RoutePrefix = string.Empty;
            });
        }
    }
}