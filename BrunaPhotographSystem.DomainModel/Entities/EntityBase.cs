using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public abstract class EntityBase<EntityId>
    {
        [Key]
        public EntityId Id { get; set; }
    }
}
