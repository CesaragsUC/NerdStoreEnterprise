using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extentions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Service
{
    public class CatalogoService :Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);

            _httpClient = httpClient;
        }

        public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");
            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<PagedViewModel<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");
            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<ProdutoViewModel>(response);
        }
    }
}
