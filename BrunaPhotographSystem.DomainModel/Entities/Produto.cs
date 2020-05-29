using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class Produto: EntityBase<Guid>
    {
        
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public Produto()
        {
            Id = Guid.NewGuid();
        }
    }
}
