using System;
using System.Collections.Generic;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class FotoProduto: EntityBase<Guid>
    {
        public Foto Foto { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public FotoProduto()
        {

            Id = Guid.NewGuid();
            Foto = new Foto();
            Produto = new Produto();
        }
    }
}
