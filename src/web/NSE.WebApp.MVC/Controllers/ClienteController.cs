using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Service;

namespace NSE.WebApp.MVC.Controllers
{
    public class ClienteController : MainController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> NovoEndereco(EnderecoViewModel endereco)
        {
            var response = await _clienteService.AdicionarEndereco(endereco);

            if (ResponsePossuiErros(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("EnderecoEntrega", "Pedido");
        }
    }
}
