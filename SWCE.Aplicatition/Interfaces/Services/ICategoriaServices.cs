using SWCE.Application.Base;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static SWCE.Application.Base.IServiceBase;

namespace SWCE.Application.Interfaces.Services
{
    public interface ICategoriaServices : IServiceBase<Categoria, CreateCategoriaDto, UpdateCategoriaDto, DisableCategoriaDto>   
    {
        Task<List<Categoria>> ObtenerActivasAsync();
    }
}
