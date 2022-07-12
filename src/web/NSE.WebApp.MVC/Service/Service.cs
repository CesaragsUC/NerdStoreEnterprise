using NSE.WebApp.MVC.Extentions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Service
{
    public abstract class Service
    {

        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeseralizaObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), option);
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw  new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
    }
}
