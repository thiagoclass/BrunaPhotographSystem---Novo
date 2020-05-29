//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class PedidoFotoProdutoADORepository : IPedidoFotoProdutoRepository
//    {
        
//        public readonly IFotoProdutoRepository fotoProdutoRepository;

//        public PedidoFotoProdutoADORepository(IFotoProdutoRepository fotoProdutoRepository)
//        {
//            this.fotoProdutoRepository = fotoProdutoRepository;
//        }
//        public void Create(PedidoFotoProduto entity)
//        {
            
        
//                string SQL = "INSERT INTO PedidoFotoProduto (Id, Pedido, FotoProduto) Values ('"
//                    + entity.Id.ToString() + "', '" + entity.Pedido.Id.ToString() + "', '" + entity.FotoProduto.Id.ToString() + "')";

//                BDSQL.ExecutarComandoSQL(SQL);
            
//        }

//        public void Delete(Guid id)
//        {

//            string SQL = "DELETE FROM PedidoFotoProduto WHERE Id = '" + id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);

//        }

//        public PedidoFotoProduto Read(Guid id)
//        {
//            PedidoFotoProduto pedidoFotoProduto = new PedidoFotoProduto();
//            string SQL = "SELECT * FROM PedidoFotoProduto WHERE id = '" + id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                pedidoFotoProduto.Id = Guid.Parse(registro[0]["Id"]);
//                pedidoFotoProduto.Pedido = new Pedido() { Id = (Guid.Parse(registro[0]["Pedido"])) };
//                pedidoFotoProduto.FotoProduto = fotoProdutoRepository.Read(Guid.Parse(registro[0]["FotoProduto"]));
                
//            }
//            return pedidoFotoProduto;
//        }

//        public IEnumerable<PedidoFotoProduto> ReadAll()
//        {
//            List<PedidoFotoProduto> listPedidoFotoProduto = new List<PedidoFotoProduto>();
//            string SQL = "SELECT * FROM PedidoFotoProduto";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                PedidoFotoProduto pedidoFotoProduto = new PedidoFotoProduto();
//                pedidoFotoProduto.Id = Guid.Parse(registro["Id"]);
//                pedidoFotoProduto.Pedido = new Pedido() { Id = Guid.Parse(registro["Pedido"])};
//                pedidoFotoProduto.FotoProduto = fotoProdutoRepository.Read(Guid.Parse(registro["FotoProduto"]));

//                listPedidoFotoProduto.Add(pedidoFotoProduto);
//            }
//            return listPedidoFotoProduto;
//        }
        

//        public void Update(PedidoFotoProduto entity)
//        {

//            string SQL = "UPDATE PedidoFotoProduto SET Pedido = '" + entity.Pedido.Id.ToString() +
//                "', FotoProduto = '" + entity.FotoProduto.Id.ToString() +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
