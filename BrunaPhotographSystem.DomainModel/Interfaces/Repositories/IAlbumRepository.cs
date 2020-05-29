using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Repositories
{
    public interface IAlbumRepository : IRepository<Album, Guid>
    {
        
        Album Read(String nomeAlbum);


    }
}
