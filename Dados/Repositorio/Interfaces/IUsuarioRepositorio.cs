﻿

using LivroShop.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivroShop.Dados.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<int> AdicionarAsync(Usuario usuario);

        Task<IEnumerable<Usuario>> RecuperarTodosAsync();

        Task<bool> VerificarUsuarioAsync(string email, string senha);

        Task<Usuario> RecuperarPorCredenciais(string email, string senha);


    }
}
