using Newtonsoft.Json;
using BrunaPhotographSystem.DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class Cliente : EntityBase<Guid>
    {

        public string Nome { get; set; }
        public CPF CPF { get; set; }
        public String Email { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Album> Albuns {get; set;}
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Pedido> Pedidos { get; set; }
    }
}
