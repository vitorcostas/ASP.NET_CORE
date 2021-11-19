
using LivroShop.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LivroShop.Dados.Contexto.Gerador
{
    public class GeradorDados
    {
        public static void InicializarDados(IServiceProvider provedorServico)
        {
            using (var contexto = new LivroBdContexto(provedorServico.GetRequiredService<DbContextOptions<LivroBdContexto>>()))
            {
                if  (contexto.Livros.Any()) 
                {
                    return; 
                }

                contexto.Livros.AddRange
                    (
                        new Livro
                        {
                            Id = 1,
                            Titulo = "Memorias Postumas",
                            Autor = "Machado de Assis",
                            Edicao = 2,
                            Editora = "Letras",
                            ISBN = "123456634"
                        },
                        new Livro
                        {
                            Id = 2,
                            Titulo = "Capitaes da Areia",
                            Autor = "Machado de Assis",
                            Edicao = 4,
                            Editora = "Letras",
                            ISBN = "1234423566"
                        }
                    );
                contexto.SaveChanges();
            }

        }
    }
}
