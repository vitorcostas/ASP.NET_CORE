

using LivroShop.Modelos;
using LivroShop.Servicos.Auth.Jwt;
using System;

namespace LivroShop.Adaptadores
{
    public static class ModeloAdaptadores
    {
        public static JwtCredenciais ParaJwtCredenciais(this Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException();
            }
            return new JwtCredenciais
            {
                Email = usuario.Email,
                Senha = usuario.Senha,
                Role = usuario.Role
            };
        }
    }
}
