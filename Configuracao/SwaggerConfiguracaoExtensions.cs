

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LivroShop.Configuracao
{
    public static class SwaggerConfiguracaoExtensions
    {
        public static void AdicionarConfiguracaoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Livros Shop",
                    Description = "Api responsavel por cadastrar livros",
                    Contact = new OpenApiContact { Name = "Livro Shop", Email = "contato@livroshop.com.br" }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorizacao JWT via header (requisicao) utilizando o scheme Bearer. Exemplo : \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement { 
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string [] {}
                    }
                });
            });
        }

        public static void UseConfiguracaoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("v1/swagger.json", "v1");   
            });

        }
    }
}
