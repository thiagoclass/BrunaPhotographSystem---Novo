//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class AlbumADORepository : IAlbumRepository
//    {
       

//        public void Create(Album entity)
//        {
//            string SQL = "INSERT INTO Album (ID, Nome, Descricao,Cliente) Values ('"
//                + entity.Id.ToString() + "', '" + entity.Nome + "', '" + entity.Descricao + "', '" + entity.Cliente.Id.ToString() + "')";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public void Delete(Guid id)
//        {
//            string SQL = "DELETE FROM Album WHERE ID = '" + id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public Album Read(Guid id)
//        {
//            Album album = new Album();
//            string SQL = "SELECT * FROM Album WHERE ID = '" + id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                album.Id = Guid.Parse(registro[0]["ID"]);
//                album.Nome = registro[0]["Nome"];
//                album.Descricao = registro[0]["Descricao"];
//            }
//            return album;
//        }
//        public Album Read(String nomeAlbum)
//        {
//            Album album = new Album();
//            string SQL = "SELECT * FROM Album WHERE Nome = '" + nomeAlbum + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                album.Id = Guid.Parse(registro[0]["ID"]);
//                album.Nome = registro[0]["Nome"];
//                album.Descricao = registro[0]["Descricao"];
//            }
//            return album;
//        }

//        public IEnumerable<Album> ReadAll()
//        {
//            List<Album> listAlbum = new List<Album>();
//            string SQL = "SELECT * FROM Album";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                Album album = new Album();
//                album.Id = Guid.Parse(registro["ID"]);
//                album.Nome = registro["Nome"];
//                album.Descricao = registro["Descricao"];
//                //album.Cliente = new ClienteADORepository().Read(Guid.Parse(registro["Cliente"]));

//                listAlbum.Add(album);
//            }
//            return listAlbum;
//        }
        

//        public void Update(Album entity)
//        {

//            string SQL = "UPDATE Album SET Nome = '" + entity.Nome +
//                "', Descricao = '" + entity.Descricao +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
