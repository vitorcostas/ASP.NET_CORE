

using LivroShop.Servicos.Auth.Jwt.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LivroShop.Servicos.Auth.Jwt
{
    public class JwtAuthGerenciador : IJwtAuthGerenciador
    {

        #region Campos
        private readonly JwtConfiguracoes jwtConfiguracoes;

        #endregion

        #region Construtor
        public JwtAuthGerenciador(IOptions<JwtConfiguracoes>  jwtConfiguracoes)
        {
            this.jwtConfiguracoes = jwtConfiguracoes.Value;

        }
        #endregion
        public JwtAuthModelo GerarToken(JwtCredenciais credenciais)
        {
            var declaracoes = new List<Claim>
            {
                new Claim(ClaimTypes.Email, credenciais.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, credenciais.Role)
            };

            var segredo = Encoding.ASCII.GetBytes(jwtConfiguracoes.Segredo);

            var jwtToken = new JwtSecurityToken(
                    jwtConfiguracoes.Emissor,
                    jwtConfiguracoes.Audiencia,
                    declaracoes,
                    expires: DateTime.Now.AddMinutes(jwtConfiguracoes.ValorMinutos),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(segredo), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


            return new JwtAuthModelo
            {
                TokenAcesso = accessToken,
                TekenType = "bearer",
                ExpiraEm = jwtConfiguracoes.ValorMinutos * 60
            };
        }
    }
}
