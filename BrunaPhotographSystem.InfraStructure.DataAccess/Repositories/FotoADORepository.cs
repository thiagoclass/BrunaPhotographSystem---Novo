//using Microsoft.AspNetCore.Http;
//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;


//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class FotoADORepository : IFotoRepository
//    {
       

//        public void Create(Foto entity)
//        {
            
//            List<SqlParameter> parametros = new List<SqlParameter>();
//            SqlParameter parametro = new SqlParameter();
//            parametro.ParameterName = "@ID";
//            parametro.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
//            parametro.Value = Guid.NewGuid();
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Album";
//            parametro.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
//            parametro.Value = entity.Album.Id;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Nome";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.Nome;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Descricao";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.Descricao;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Situacao";
//            parametro.SqlDbType = System.Data.SqlDbType.Int;
//            parametro.Value = entity.Situacao==null?0:entity.Situacao;
//            parametros.Add(parametro);
//            //parametro = new SqlParameter();
//            //parametro.ParameterName = "@Imagem";
//            //parametro.SqlDbType = System.Data.SqlDbType.Image;
//            //parametro.Value = entity.Imagem;
//            //parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@fotourl";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.FotoUrl;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Minifotourl";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.MiniFotoUrl;
//            parametros.Add(parametro);

//            string SQL = "INSERT INTO Foto (Id, Album, Nome, descricao,Situacao, FotoUrl, MiniFotoUrl) Values (@ID,@Album,@Nome,@Descricao,@situacao,@fotourl,@MiniFotoUrl)";
            

//            BDSQL.ExecutarComandoSQL(SQL,parametros.ToArray());
//        }
//        public void CreateWithId(Foto entity)
//        {

//            List<SqlParameter> parametros = new List<SqlParameter>();
//            SqlParameter parametro = new SqlParameter();
//            parametro.ParameterName = "@ID";
//            parametro.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
//            parametro.Value = entity.Id;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Album";
//            parametro.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
//            parametro.Value = entity.Album.Id;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Nome";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.Nome;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Descricao";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.Descricao;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Situacao";
//            parametro.SqlDbType = System.Data.SqlDbType.Int;
//            parametro.Value = entity.Situacao == null ? 0 : entity.Situacao;
//            parametros.Add(parametro);
//            //parametro = new SqlParameter();
//            //parametro.ParameterName = "@Imagem";
//            //parametro.SqlDbType = System.Data.SqlDbType.Image;
//            //parametro.Value = entity.Imagem;
//            //parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@fotourl";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.FotoUrl;
//            parametros.Add(parametro);
//            parametro = new SqlParameter();
//            parametro.ParameterName = "@Minifotourl";
//            parametro.SqlDbType = System.Data.SqlDbType.VarChar;
//            parametro.Value = entity.MiniFotoUrl;
//            parametros.Add(parametro);

//            string SQL = "INSERT INTO Foto (Id, Album, Nome, descricao,Situacao, FotoUrl, MiniFotoUrl) Values (@ID, @Album, @Nome, @Descricao, @situacao, @fotourl, @minifotourl)";


//            BDSQL.ExecutarComandoSQL(SQL, parametros.ToArray());
//        }
        
//        public void Delete(Guid id)
//        {
//            string SQL = "DELETE FROM Foto WHERE Id = '" + id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
            
//        }

//        public Foto Read(Guid id)
//        {
//            Foto foto = new Foto();
//            string SQL = "SELECT * FROM Foto WHERE id = '" + id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                foto.Id = Guid.Parse(registro[0]["ID"]);
//                foto.Album = new AlbumADORepository().Read(Guid.Parse(registro[0]["Album"]));
//                foto.Nome = registro[0]["Nome"];
//                foto.Descricao = registro[0]["Descricao"];
//                foto.Situacao = Int32.Parse(registro[0]["Situacao"]);
//                //foto.Imagem = Encoding.ASCII.GetBytes(registro[0]["Imagem"]);
//                foto.FotoUrl = registro[0]["FotoUrl"];
//                foto.MiniFotoUrl = registro[0]["MiniFotoUrl"];
//            }
//            return foto;
//        }
//        public IEnumerable<Foto> ReadAll()
//        {
//            List<Foto> listFoto = new List<Foto>();
//            string SQL = "SELECT * FROM Foto";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                Foto foto = new Foto();
//                foto.Id = Guid.Parse(registro["ID"]);
//                foto.Album = new AlbumADORepository().Read(Guid.Parse(registro["Album"]));
//                foto.Nome = registro["Nome"];
//                foto.Descricao = registro["Descricao"];
//                foto.Situacao = Int32.Parse(registro["Situacao"]);
//                foto.FotoUrl = registro["FotoUrl"];
//                foto.MiniFotoUrl = registro["MiniFotoUrl"];

//                listFoto.Add(foto);
//            }
//            return listFoto;
//        }
//        ////////public IEnumerable<Foto> ReadAll()
//        ////////{
//        ////////    List<Foto> listFoto = new List<Foto>();
//        ////////    string SQL = "SELECT * FROM Foto";
//        ////////    var registros = BDSQL.ExecutarComandoLeituraSQLDataTable(SQL);

//        ////////    for(int i =0; i<= registros.Rows.Count-1;i++)
//        ////////    {

//        ////////        Foto foto = new Foto();

//        ////////        foto.Id = (Guid)registros.Rows[i]["ID"];
//        ////////        foto.Album = new AlbumADORepository().Read((Guid)registros.Rows[i]["Album"]);
//        ////////        foto.Nome = registros.Rows[i]["Nome"].ToString();
//        ////////        foto.Descricao = registros.Rows[i]["Descricao"].ToString();
//        ////////        foto.Situacao = (int)registros.Rows[i]["Situacao"];
//        ////////        //foto.Imagem = (byte[])registros.Rows[i]["Imagem"];
//        ////////        foto.FotoUrl = registros.Rows[i]["FotoUrl"].ToString();
//        ////////        foto.MiniFotoUrl = registros.Rows[i]["MiniFotoUrl"].ToString();
//        ////////        listFoto.Add(foto);
//        ////////    }

//        ////////    return listFoto;
//        ////////}

//        public void Update(Foto entity)
//        {

//            string SQL = "UPDATE Foto SET Nome = '" + entity.Nome.ToString() +
//                "', Descricao = '" + entity.Descricao.ToString() +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//        public void AtualizarSituacao(Foto entity)
//        {

//            string SQL = "UPDATE Foto SET Situacao = '" + entity.Situacao.ToString() +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
