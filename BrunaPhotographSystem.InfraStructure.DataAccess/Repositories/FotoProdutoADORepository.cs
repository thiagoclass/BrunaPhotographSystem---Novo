//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class FotoProdutoADORepository : IFotoProdutoRepository
//    {
//        public readonly IFotoRepository fotoRepository;
//        public readonly IProdutoRepository produtoRepository;
//        public FotoProdutoADORepository(IFotoRepository fotoRepository,IProdutoRepository produtoRepository)
//        {
//            this.fotoRepository = fotoRepository;
//            this.produtoRepository = produtoRepository;
//        }
//        public void Create(FotoProduto entity)
//        {


//            string SQL = "INSERT INTO FotoProduto (Id, Foto, Produto, Quantidade) Values ('"
//                + entity.Id.ToString() + "', '" + entity.Foto.Id.ToString() + "', '" + entity.Produto.Id.ToString() + "', '" + entity.Quantidade.ToString() + "')";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public void Delete(Guid id)
//        {

//            string SQL = "DELETE FROM FotoProduto WHERE Id = '" + id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);

//        }

//        public FotoProduto Read(Guid id)
//        {
//            FotoProduto fotoProduto = new FotoProduto();
//            string SQL = "SELECT * FROM FotoProduto WHERE id = '" + id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                fotoProduto.Id = Guid.Parse(registro[0]["Id"]);
//                fotoProduto.Foto = fotoRepository.Read(Guid.Parse(registro[0]["Foto"]));
//                fotoProduto.Produto = produtoRepository.Read(Guid.Parse(registro[0]["Produto"]));
//                fotoProduto.Quantidade = Int32.Parse(registro[0]["Quantidade"]);
//            }
//            return fotoProduto;
//        }

//        public IEnumerable<FotoProduto> ReadAll()
//        {
//            List<FotoProduto> listFotoProduto = new List<FotoProduto>();
//            string SQL = "SELECT * FROM FotoProduto";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                FotoProduto fotoProduto = new FotoProduto();
//                fotoProduto.Id = Guid.Parse(registro["Id"]);
//                fotoProduto.Foto = fotoRepository.Read(Guid.Parse(registro["Foto"]));
//                fotoProduto.Produto = produtoRepository.Read(Guid.Parse(registro["Produto"]));
//                fotoProduto.Quantidade = Int32.Parse(registro["Quantidade"]);

//                listFotoProduto.Add(fotoProduto);
//            }
//            return listFotoProduto;
//        }

//        public void Update(FotoProduto entity)
//        {

//            string SQL = "UPDATE FotoProduto SET Foto = '" + entity.Foto.Id.ToString() +
//                "', Produto = '" + entity.Produto.Id.ToString() +
//                "', Quantidade = '" + entity.Quantidade.ToString() +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
