using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Services
{
    public interface IFotoService : IServiceBase<Foto, Guid>
    {
        void CriarComId(Foto entity);
        IEnumerable<Foto> BuscarTodosDoAlbumPeloNome(String album);
        IEnumerable<Foto> BuscarTodosDoAlbum(Album album);


    }
}
