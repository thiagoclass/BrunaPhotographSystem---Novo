using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class Foto : EntityBase<Guid>
    {
        public Album Album { get; set; }
        //public byte[] Imagem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Situacao { get; set; }
        public string FotoUrl { get; set; }
        public string MiniFotoUrl { get; set; }
        public Foto()
        {
            Album = new Album();
        }
    }
}
