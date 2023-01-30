using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfaceServico;
using Dominio.Servicos;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Generico;
using Infraestrutura.Repositorio.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_GestaoProdutos", Version = "v1" });
            });

            //Pega a string de conexão contidade no appetings.json
            services.AddDbContext<ContextoBase>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            //Interface e repositorio
            services.AddSingleton(typeof(IGenericos<>), typeof(RepositorioGenerico<>));
            services.AddSingleton<IProduto, RepositorioProduto>();

            //Servico dominio
            services.AddSingleton<IServicoProduto, ServicoProdutos>();

            //Interface aplicação
            services.AddSingleton<IAplicacaoProdutos, AplicacaoProduto>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_GestaoProdutos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
