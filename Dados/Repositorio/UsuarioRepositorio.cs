using LivroShop.Dados.Repositorio.Interfaces;
using LivroShop.Modelos;
using System;
using System.Threading.Tasks;
using LivroShop.Dados.Contexto;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LivroShop.Dados.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        #region Campos

        private readonly LivroBdContexto contexto;

        #endregion

        #region Construtor

        public UsuarioRepositorio(LivroBdContexto contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Metodos
        public async Task<int> AdicionarAsync(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);

            return await contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> RecuperarTodosAsync()
        {
            return await contexto.Usuarios.ToListAsync();
        }
        public async Task<Usuario> RecuperarPorCredenciais(string email, string senha)
        {
            return await contexto.Usuarios.SingleOrDefaultAsync(usuario => usuario.Email == email && usuario.Senha == senha);

        }
        public async Task<bool> VerificarUsuarioAsync(string email, string senha)
        {
            var usuario = await contexto.Usuarios.SingleOrDefaultAsync(usuario => usuario.Email == email && usuario.Senha == senha);

            return usuario == null;
        }

        #endregion
        
    }
}
