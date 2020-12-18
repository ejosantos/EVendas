using System;
using Avaliacao.EVenda.Dominio.Repositorios;
using Avaliacao.EVenda.Dominio.Servicos;
using Avaliacao.EVenda.Repositorio.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using AutoMapper;
using Avaliacao.EVenda.ServiceBus;

namespace Avaliacao.Venda
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto final Aceleração (VENDAS).", Version = "v1" });
            });

            services.AddDbContext<EVendaContext>(options => options.UseInMemoryDatabase(databaseName: "AvaliacaoDB"));
            services.AddTransient(typeof(IRepositorio<>), typeof(BaseRepositorio<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProdutoVendaFactory, ProdutoVendaFactory>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IVendaRepositorio, VendaRepositorio>();
            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IProdutoVendidoServiceBus, ProdutoVendidoServiceBus>();
            services.AddSingleton<IProdutoMessageServices, ProdutoMessageServices>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
          
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var bus = app.ApplicationServices.GetService<IProdutoMessageServices>();

            bus.RegisterOnMessageHandlerAndReceiveMessagesProdutoAtualizado();
            bus.RegisterOnMessageHandlerAndReceiveMessagesProdutoCriado();

        }
    }
}
