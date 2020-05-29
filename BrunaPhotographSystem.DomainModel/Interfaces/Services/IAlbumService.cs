using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Services
{
    public interface IAlbumService : IServiceBase<Album, Guid>
    {
        Album BuscarPeloNome(String nomeAlbum);
        List<Album> BuscarTodosDoCliente(Cliente cliente);
    }
}
