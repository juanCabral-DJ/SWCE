using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static SWCE.Application.Base.IServiceBase;

namespace SWCE.Application.Interfaces.Services
{
    public interface IProductoServices : IServiceBase<Producto, CreateProductoDto, UpdateProductoDto, DisableProductoDto>
    {
        
    }
}
