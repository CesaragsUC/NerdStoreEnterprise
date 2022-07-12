using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pedidos.API.Application.Queries;

namespace NSE.Pedidos.API.Services
{
    public class PedidoOrquestradorIntegrationHandler :  IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PedidoOrquestradorIntegrationHandler> _logger;
        private Timer _timer;

        public PedidoOrquestradorIntegrationHandler(ILogger<PedidoOrquestradorIntegrationHandler> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço de pedidos iniciado.");

            _timer = new Timer(Processarpedido,null,TimeSpan.Zero, TimeSpan.FromSeconds(15));

            await  Task.CompletedTask;
        }

        private async void Processarpedido(object state)
        {
            _logger.LogInformation("Processando pedidos...");

            using (var scope =_serviceProvider.CreateScope())
            {
                var pedidoQueries = scope.ServiceProvider.GetRequiredService<IPedidoQueries>();
                var pedido = await pedidoQueries.ObterPedidosAutorizados();

                if (pedido == null) return;

                var bus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                var pedidoAutorizado = new PedidoAutorizadoIntegrationEvent(pedido.ClienteId, pedido.Id,
                    pedido.PedidoItems.ToDictionary(p => p.ProdutoId, p => p.Quantidade));

                await bus.PublishAsync(pedidoAutorizado);

                _logger.LogInformation($"Pedido ID: {pedido.Id} foi encaminhado para baixa no estoque.");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço de pedido finalizado.");

            _timer?.Change(Timeout.Infinite, 0);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
