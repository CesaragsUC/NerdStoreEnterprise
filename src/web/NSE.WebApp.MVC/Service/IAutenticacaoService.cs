using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Service
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin login);
        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro registro);
        Task RealizarLogin(UsuarioRespostaLogin resposta);
        Task Logout();

        bool TokenExpirado();

        Task<bool> RefreshTokenValido();

    }
}
