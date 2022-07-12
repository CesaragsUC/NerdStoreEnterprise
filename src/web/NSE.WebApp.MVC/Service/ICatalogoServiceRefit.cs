using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models;
using Refit;

namespace NSE.WebApp.MVC.Service
{
    public interface ICatalogoServiceRefit
    {
        [Get("/catalogo/produtos")]
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();

        [Get("/catalogo/produtos/{id}")]
        Task<ProdutoViewModel> ObterPorId(Guid id);

    }
}