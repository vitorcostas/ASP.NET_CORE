using LivroShop.Modelos;
using Microsoft.EntityFrameworkCore;


namespace LivroShop.Dados.Contexto
{
    public class LivroBdContexto : DbContext
    {
        #region Construtuor
        public LivroBdContexto(DbContextOptions<LivroBdContexto> options) : base(options)
        {

        }

        #endregion

        #region Propriedades

        //Colecao que e entendida pelo EntityFramework é o DbSet
        public DbSet<Livro> Livros { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        #endregion

    }
}
