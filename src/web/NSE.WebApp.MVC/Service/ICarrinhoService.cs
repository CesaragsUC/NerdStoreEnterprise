using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Service
{
    public interface IComprasBffService
    {
        // Carrinho
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel carrinho);
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel carrinho);
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AplicarVoucherCarrinho(string voucher);

        // Pedido
        Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao);
        Task<PedidoViewModel> ObterUltimoPedido();
        Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId();
        PedidoTransacaoViewModel MapearParaPedido(CarrinhoViewModel carrinho, EnderecoViewModel endereco);
    }
}
