using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                foreach (var message in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty,message);
                }

                return true;
            }

            return false;
        }

        protected void AdicionarErroValidacao(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        protected bool OperacaoValida()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
