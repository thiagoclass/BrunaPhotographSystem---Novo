using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrunaPhotographSystem.DomainModel.Interfaces.CQRS;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using BrunaPhotographSystem.DomainService.Services;
using BrunaPhotographSystem.InfraStructure.AzureQueue;
using BrunaPhotographSystem.InfraStructure.DataAccess.Context;
using BrunaPhotographSystem.InfraStructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BrunaPhotographSystem.Order
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
            services.AddDbContext<FotografaContext>(options =>
            options.UseSqlServer(
            InfraStructure.DataAccess.Properties.Resources.ConnectionString));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;
            services.AddTransient<IClienteRepository, ClienteEntityRepository>();
            services.AddTransient<IFotoRepository, FotoEntityRepository>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<IAlbumRepository, AlbumEntityRepository>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IProdutoRepository, ProdutoEntityRepository>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IPedidoRepository, PedidoEntityRepository>();
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IFotoProdutoRepository, FotoProdutoEntityRepository>();
            services.AddTransient<IFotoProdutoService, FotoProdutoService>();
            services.AddTransient<IPedidoFotoProdutoRepository, PedidoFotoProdutoEntityRepository>();
            services.AddTransient<IPedidoFotoProdutoService, PedidoFotoProdutoService>();
            services.AddTransient<IQueue, AzureStorageQueue>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("brunaphotographsystem-webapi-authentication-valid")),
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = "BrunaPhotographSystem",
                    ValidAudience = "Postman",
                };
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
