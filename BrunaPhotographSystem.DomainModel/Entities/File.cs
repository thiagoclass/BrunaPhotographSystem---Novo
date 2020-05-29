using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BrunaPhotographSystem.DomainModel.Entities
{
    public class File
    {
        public string FileName { get; set; }
        public Stream FileContent { get; set; }
        public string BlobContainerName { get; set; }
        public string ContentType { get; set; }
        public string Directory { get; set; }
 
    }
}
