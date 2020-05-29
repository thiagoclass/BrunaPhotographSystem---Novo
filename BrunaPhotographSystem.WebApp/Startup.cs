using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using BrunaPhotographSystem;
using BrunaPhotographSystem.ApiClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using BrunaPhotographSystem.DomainModel.Interfaces.CQRS;
using BrunaPhotographSystem.InfraStructure.AzureQueue;

namespace BrunaPhotographSystem
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                }, ArrayPool<char>.Shared));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddTransient<IQueue, AzureStorageQueue>();
            services.AddHttpClient<CoreClienteClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/api/");
            });
            services.AddHttpClient<CoreProdutoClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/api/");
            });
            services.AddHttpClient<CoreAlbumClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/api/");
            });
            services.AddHttpClient<CoreFotoClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/api/");
            });
            services.AddHttpClient<OrderPedidoClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5007/api/");
            });
            services.AddHttpClient<OrderPedidoFotoProdutoClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5007/api/");
            });
            services.AddHttpClient<OrderFotoProdutoClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5007/api/");
            });
            services.AddHttpClient<IAmClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Default}/{action=Responsivo}");
            });
        }
    }
}
