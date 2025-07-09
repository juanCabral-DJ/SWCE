using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SWCE.Application.Dtos.AdministracionModule.PedidoDtos;
using SWCE.Application.Validators.PedidoValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IRepositorioPedido
    {
        private readonly E_commerceContext _context;
        private readonly ILoggerBase<Pedido> _logger;
        private readonly CancelarPedidoValidator _validator;

        public PedidoRepository(E_commerceContext context, ILoggerBase<Pedido> logger, CancelarPedidoValidator validator)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo pedido por ID: {Id}", id);
                var pedido = await base.GetbyIdasync(id);
                return OperationResult.Success("Pedido obtenido correctamente.", pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener el pedido: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener el pedido.");
            }
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<Pedido, bool>> filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los pedidos.");
                var pedidos = await base.GetAllasync(filter);
                return OperationResult.Success("Pedidos obtenidos correctamente.", pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los pedidos: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener los pedidos.");
            }
        }

        public async override Task<OperationResult> Createasync(Pedido entity)
        {
            try
            {
                if (entity == null)
                    return OperationResult.Failure("El pedido no puede ser nulo.");

                return await base.Createasync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al crear el pedido: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al crear el pedido.");
            }
        }

        public async override Task<OperationResult> Updateasync(Pedido entity)
        {
            try
            {
                if (entity == null)
                    return OperationResult.Failure("El pedido no puede ser nulo.");

                return await base.Updateasync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar el pedido: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al actualizar el pedido.");
            }
        }

        public async override Task<bool> ExistsAsync(Expression<Func<Pedido, bool>> filter)
        {
            return await base.ExistsAsync(filter);
        }

        public async Task<Pedido> CancelarPedido(int pedidoId)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return null;

            var dto = new CancelarPedidoDto { PedidoId = pedidoId};
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                _logger.LogError("Validación fallida al cancelar pedido");
                return null;
            }

            pedido.Estado = "Cancelado";
            await _context.SaveChangesAsync();
            return pedido;
        }
    }
}
