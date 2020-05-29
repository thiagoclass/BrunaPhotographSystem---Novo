using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Repositories
{
    public interface IFotoRepository : IRepository<Foto, Guid>
    {
        
        void CreateWithId(Foto foto);
        //void AtualizarSituacao(Foto entity);
    }
}
