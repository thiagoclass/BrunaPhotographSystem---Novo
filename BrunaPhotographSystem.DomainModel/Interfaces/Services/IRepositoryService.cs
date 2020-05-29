using BrunaPhotographSystem.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Interfaces.Services
{
    public interface IServiceBase<T, EntityId> where T : EntityBase<EntityId>
    {
        void Criar(T entity);
        T Buscar(EntityId id);
        IEnumerable<T> BuscarTodos();
        void Atualizar(T entity);
        void Deletar(EntityId id);
    }
}
