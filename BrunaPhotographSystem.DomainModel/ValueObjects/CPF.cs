using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.ValueObjects
{
    public class CPF
    {   [Key]
        public int Numero { get; set; }
    }
}
