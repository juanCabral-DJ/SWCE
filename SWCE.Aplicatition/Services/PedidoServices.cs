using Microsoft.Extensions.Configuration;
using SWCE.Application.Base;
using SWCE.Application.Base.AdministrationModuleMappers;
using SWCE.Application.Dtos.AdministracionModule.PedidoDtos;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;

namespace SWCE.Application.Services
{
    public sealed class PedidoServices : IPedidoServices
    {
        private readonly IRepositorioPedido _pedidoRepo;
        private readonly ILoggerBase<PedidoServices> _logger;
        private readonly PedidoMapper _mapper;
        private readonly IConfiguration _configuration;

        public PedidoServices(
            IRepositorioPedido pedidoRepo,
            ILoggerBase<PedidoServices> logger,
            PedidoMapper mapper,
            IConfiguration configuration)
        {
            _pedidoRepo = pedidoRepo;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetbyId(int id)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Fetching Pedido by ID: {Id}", id);

                result = await _pedidoRepo.GetbyIdasync(id);

                _logger.LogInformation("Successfully fetched Pedido with ID: {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Pedido by ID.", ex);
                result = OperationResult.Failure("An error occurred while retrieving the Pedido.");
            }

            return result;
        }

        public async Task<OperationResult> GetPedidosByClienteId(int clienteId)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Fetching Pedidos for ClienteId: {ClienteId}", clienteId);

                result = await _pedidoRepo.GetAllasync(p => p.id == clienteId); 

                _logger.LogInformation("Successfully fetched Pedidos for ClienteId: {ClienteId}", clienteId);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Pedidos by ClienteId.", ex);
                result = OperationResult.Failure("An error occurred while retrieving Pedidos.");
            }

            return result;
        }

        public async Task<OperationResult> CancelarPedido(CancelarPedidoDto dto)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Attempting to cancel Pedido with ID: {PedidoId}", dto.PedidoId);

                var operation = await _pedidoRepo.GetbyIdasync(dto.PedidoId);
                if (operation == null || !operation.IsSuccess || operation.Data is not Pedido pedido)
                {
                    _logger.LogError("Pedido not found");
                    return OperationResult.Failure("Pedido not found.");
                }

                pedido.Cancelar("Cancelado por solicitud del cliente");

                result = await _pedidoRepo.Updateasync(pedido);

                _logger.LogInformation("Successfully canceled Pedido with ID: {PedidoId}", dto.PedidoId);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while cancelling the Pedido.", ex);
                result = OperationResult.Failure("An error occurred while cancelling the Pedido.");
            }

            return result;
        }
    }
}
