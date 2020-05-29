//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BrunaPhotographSystem.DomainModel.CQRS.Commands;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using BrunaPhotographSystem.DomainService.Services;
//using BrunaPhotographSystem.InfraStructure.DataAccess.Context;
//using BrunaPhotographSystem.InfraStructure.DataAccess.Repositories;
//using Microsoft.Azure.WebJobs;
//using Newtonsoft.Json;

//namespace BrunaPhotographSystem.Order.WebJob
//{
//    public class Functions
//    {
//        private static CommandHandler _commandHandler;

//        public Functions()
//        {
//            _commandHandler = new CommandHandler(
//                                                    new PedidoService(
//                                                                        new PedidoEntityRepository(
//                                                                                                    new FotografaContext())
//                                                   ,new PedidoFotoProdutoService(
//                                                                        new PedidoFotoProdutoEntityRepository(
//                                                                                                    new FotografaContext()))
//                                                   ,new FotoProdutoService(
//                                                                        new FotoProdutoEntityRepository(
//                                                                                                    new FotoEntityRepository(
//                                                                                                                        new FotografaContext())
//                                                                                                  , new ProdutoEntityRepository(
//                                                                                                                        new FotografaContext())
//                                                                                                  , new FotografaContext()))
//                                                   ,new ProdutoService(
//                                                                        new ProdutoEntityRepository(
//                                                                                                    new FotografaContext()))));
//        }

//        // This function will get triggered/executed when a new message is written 
//        // on an Azure Queue called queue.
//        public static void ProcessQueueMessage([QueueTrigger("add-pedido-command-queue")] string message, TextWriter log)
//        {
//            new Functions();
//            var command = JsonConvert.DeserializeObject<AddPedidoCommand>(message);
//            _commandHandler.Handle(command);
//        }
//    }
//}
