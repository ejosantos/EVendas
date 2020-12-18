using System;
using Avaliacao.Estoque.Infra.Contexto;
using Avaliacao.Estoque.Servicos;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Avaliacao.Estoque
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto final Aceleração (ESTOQUE).", Version = "v1" });
            });

            services.AddDbContext<EstoqueContext>(options => options.UseInMemoryDatabase(databaseName: "AvaliacaoEstoqueDB"));
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoServiceBus, ProdutoServiceBus>();
            services.AddSingleton<IProdutoAtualizacaoServiceBus, ProdutoAtualizacaoServiceBus>();

            services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).AddFluentValidation(f => f.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            var bus = app.ApplicationServices.GetService<IProdutoAtualizacaoServiceBus>();

            bus.RegisterOnMessageHandlerAndReceiveMessagesProdutoVenda();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
