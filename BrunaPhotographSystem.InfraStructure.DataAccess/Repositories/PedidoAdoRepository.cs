//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class PedidoADORepository : IPedidoRepository
//    {
//        public readonly IClienteRepository clienteRepository;
//        public readonly IPedidoFotoProdutoRepository pedidoFotoProdutoRepository;
//        public readonly IFotoProdutoRepository fotoProdutoRepository;
//        public PedidoADORepository(IClienteRepository clienteRepository, IPedidoFotoProdutoRepository pedidoFotoProdutoRepository, IFotoProdutoRepository fotoProdutoRepository)
//        {
//            this.clienteRepository = clienteRepository;
//            this.pedidoFotoProdutoRepository = pedidoFotoProdutoRepository;
//            this.fotoProdutoRepository = fotoProdutoRepository;
//        }
//        public void Create(Pedido entity)
//        {

//            string SQL = "INSERT INTO Pedido (Id, Cliente, DataPedido) Values ('"
//                + entity.Id.ToString() + "', '" + entity.Cliente.Id.ToString() + "', '" + entity.DataPedido.ToString("yyyyMMdd") + "')";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public void Delete(Guid id)
//        {
//            foreach(var pedidoFotoProduto in pedidoFotoProdutoRepository.ReadAll().Where(p=>p.Pedido.Id==id))
//            {
//                fotoProdutoRepository.Delete(pedidoFotoProduto.FotoProduto.Id);
//                pedidoFotoProdutoRepository.Delete(pedidoFotoProduto.Id);
//            }
//            string SQL = "DELETE FROM Pedido WHERE Id = '" + id.ToString() + "'";
//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public Pedido Read(Guid id)
//        {
//            Pedido pedido = new Pedido();
//            string SQL = "SELECT * FROM Pedido WHERE id = '" + id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                pedido.Id = Guid.Parse(registro[0]["Id"]);
//                pedido.Cliente = clienteRepository.Read(Guid.Parse(registro[0]["Cliente"]));
//                pedido.DataPedido = DateTime.Parse(registro[0]["DataPedido"]);
//            }
//            return pedido;
//        }

//        public IEnumerable<Pedido> ReadAll()
//        {
//            List<Pedido> listPedido = new List<Pedido>();
//            string SQL = "SELECT * FROM Pedido";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                Pedido pedido = new Pedido();
//                pedido.Id = Guid.Parse(registro["Id"]);
//                pedido.Cliente = clienteRepository.Read(Guid.Parse(registro["Cliente"]));
//                pedido.DataPedido = DateTime.Parse(registro["DataPedido"]);

//                listPedido.Add(pedido);
//            }
//            return listPedido;
//        }

//        public void Update(Pedido entity)
//        {

//            string SQL = "UPDATE Pedido SET Cliente = '" + entity.Cliente.Id.ToString() +
//                "', DataPedido = '" + entity.DataPedido.ToString("yyyyMMdd") +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
