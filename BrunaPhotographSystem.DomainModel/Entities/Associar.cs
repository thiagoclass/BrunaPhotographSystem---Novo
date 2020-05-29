using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class Associar
    {
        public IEnumerable<Foto> fotos { get; set; }
        public IList<PedidoFotoProduto> fotosSelecionadas { get; set; }
        public Guid fotoAssociar { get; set; }
        public Guid produto { get; set; }
        public Guid pedido { get; set; }
    }
}
