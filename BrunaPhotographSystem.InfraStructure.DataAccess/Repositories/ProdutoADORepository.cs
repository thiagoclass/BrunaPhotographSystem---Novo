//using System;
//using System.Collections.Generic;
//using System.Text;
//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class ProdutoADORepository : IProdutoRepository
//    {

//        public void Create(Produto entity)
//        {
            
            
//            string SQL = "INSERT INTO Produto (Id, Nome, Descricao) Values ('"
//                + entity.Id.ToString() + "', '" + entity.Nome.ToString() + "', '" + entity.Descricao.ToString() + "')";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public void Delete(Guid id)
//        {
            
//            string SQL = "DELETE FROM Produto WHERE Id = '" + id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
            
//        }

//        public Produto Read(Guid id)
//        {
//            Produto produto = new Produto();
//            string SQL = "SELECT * FROM Produto WHERE id = '" + id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                produto.Id = Guid.Parse(registro[0]["Id"]);
//                produto.Nome = registro[0]["Nome"];
//                produto.Descricao = registro[0]["Descricao"];
//            }
//            return produto;
//        }
        
//        public IEnumerable<Produto> ReadAll()
//        {
//            List<Produto> listProduto = new List<Produto>();
//            string SQL = "SELECT * FROM Produto";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                Produto produto = new Produto();
//                produto.Id = Guid.Parse(registro["Id"]);
//                produto.Nome = registro["Nome"];
//                produto.Descricao = registro["Descricao"];

//                listProduto.Add(produto);
//            }
//            return listProduto;
//        }

//        public void Update(Produto entity)
//        {

//            string SQL = "UPDATE Produto SET Nome = '" + entity.Nome.ToString() +
//                "', Descricao = '" + entity.Descricao.ToString() +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
