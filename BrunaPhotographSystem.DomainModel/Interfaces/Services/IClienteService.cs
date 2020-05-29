using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Services
{
    public interface IClienteService:IServiceBase<Cliente,Guid>
    {
        Cliente BuscarPeloUsuario(String Username);
    }
}
