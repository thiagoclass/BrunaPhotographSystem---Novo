using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class Album : EntityBase<Guid>
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        [ForeignKey("clienteId")]
        [Column("clienteId", TypeName= "uniqueidentifier")]
        public Cliente Cliente { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember]
        public List<Foto> Fotos { get; set; }

        public Album()
        {
            Id = Guid.NewGuid();
        }

    }
}
