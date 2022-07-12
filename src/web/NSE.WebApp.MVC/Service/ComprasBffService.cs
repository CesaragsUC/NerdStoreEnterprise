using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extentions;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NSE.Core.Communication;

namespace NSE.WebApp.MVC.Service
{
    public class ComprasBffService : Service, IComprasBffService
    {
        private readonly HttpClient _httpClient;

        public ComprasBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho/");

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<CarrinhoViewModel>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel carrinho)
        {
            var itemContent = ObterConteudo(carrinho);

            var response = await _httpClient.PostAsync("/compras/carrinho/items/", itemContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel carrinho)
        {
            var itemContent = ObterConteudo(carrinho);

            var response = await _httpClient.PutAsync($"/compras/carrinho/items/{carrinho.ProdutoId}", itemContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/compras/carrinho/items/{produtoId}");

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<int> ObterQuantidadeCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho-quantidade/");

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<int>(response);
        }

        public async Task<ResponseResult> AplicarVoucherCarrinho(string voucher)
        {
            var itemContent = ObterConteudo(voucher);

            var response = await _httpClient.PostAsync("/compras/carrinho/aplicar-voucher", itemContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        #region Pedido
        public async Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao)
        {
            var pedidoContent = ObterConteudo(pedidoTransacao);

            var response = await _httpClient.PostAsync("/compras/pedido/", pedidoContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }


        public async Task<PedidoViewModel> ObterUltimoPedido()
        {
            var response = await _httpClient.GetAsync("/compras/pedido/ultimo/");

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<PedidoViewModel>(response);
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId()
        {
            var response = await _httpClient.GetAsync("/compras/pedido/lista-cliente/");

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<IEnumerable<PedidoViewModel>>(response);
        }

        public PedidoTransacaoViewModel MapearParaPedido(CarrinhoViewModel carrinho, EnderecoViewModel endereco)
        {
            var pedido = new PedidoTransacaoViewModel
            {
                ValorTotal = carrinho.ValorTotal,
                Itens = carrinho.Itens,
                Desconto = carrinho.Desconto,
                VoucherUtilizado = carrinho.VoucherUtilizado,
                VoucherCodigo = carrinho.Voucher?.Codigo
            };

            if (endereco != null)
            {
                pedido.Endereco = new EnderecoViewModel
                {
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    Bairro = endereco.Bairro,
                    Cep = endereco.Cep,
                    Complemento = endereco.Complemento,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado
                };
            }

            return pedido;
        }
        #endregion
    }
}
