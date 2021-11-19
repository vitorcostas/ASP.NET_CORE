

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LivroShop.Dados.Contexto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LivroShop.Dados.Repositorio.Interfaces;
using LivroShop.Dados.Repositorio;
using LivroShop.Servicos.Auth.Jwt;
using LivroShop.Configuracao;
using LivroShop.Servicos.Auth.Jwt.Interfaces;

namespace LivroShop
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
            //Autenticacao
            services.AddConfiguracaoAuth(Configuration);

            //Contextos
            services.AddDbContext<LivroBdContexto>(options => options.UseInMemoryDatabase(databaseName: "LivroBD"));

            //Configuracoes
            var section = Configuration.GetSection("JwtConfiguracoes");
            services.Configure<JwtConfiguracoes>(section);

            //Repositorios
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            //Servicos
            services.AddScoped<IJwtAuthGerenciador, JwtAuthGerenciador>();

            //Swagger - documentacao API
            services.AdicionarConfiguracaoSwagger();

            //Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConfiguracaoSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
