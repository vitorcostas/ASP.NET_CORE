

namespace LivroShop.Servicos.Auth.Jwt.Interfaces
{
    public interface IJwtAuthGerenciador
    {
        JwtAuthModelo GerarToken(JwtCredenciais credenciais);
    }
}
