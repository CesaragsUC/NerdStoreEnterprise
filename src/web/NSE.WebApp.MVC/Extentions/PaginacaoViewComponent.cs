using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Extentions
{
    public class PaginacaoViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modeloPaginado)
        {
            return View(modeloPaginado);
        }
    }
}
