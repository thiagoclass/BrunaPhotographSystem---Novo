using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using BrunaPhotographSystem.ApiClient;
using BrunaPhotographSystem.DomainModel.CQRS.Commands;
using BrunaPhotographSystem.InfraStructure.AzureQueue;


namespace BrunaPhotographSystem.WebJob
{
    public class Functions
    {
        private static CommandHandler _commandHandler;

        public Functions()
        {

            //Fake Dependency Injection
            _commandHandler = new CommandHandler(new OrderPedidoClient(new HttpClient() { BaseAddress= new Uri("https://localhost:5007/api/")}, new AzureStorageQueue()), new IAmClient(new HttpClient() { BaseAddress = new Uri("https://localhost:5001/api/") }));                                                    
        }
        //new PedidoService(
        //                                                                 new PedidoEntityRepository(
        //                                                                                             new FotografaContext())
        //                                            , new PedidoFotoProdutoService(
        //                                                                 new PedidoFotoProdutoEntityRepository(
        //                                                                                             new FotografaContext()))
        //                                            , new FotoProdutoService(
        //                                                                 new FotoProdutoEntityRepository(
        //                                                                                             new FotoEntityRepository(
        //                                                                                                                 new FotografaContext())
        //                                                                                           , new ProdutoEntityRepository(
        //                                                                                                                 new FotografaContext())
        //                                                                                           , new FotografaContext()))
        //                                            , new ProdutoService(
        //                                                                 new ProdutoEntityRepository(
        //                                                                                             new FotografaContext()))));

        public static void ProcessAddProductCommandQueueMessage([QueueTrigger(AddPedidoCommand.ConstQueueName)] string message, ILogger logger)
        {
            logger.LogInformation($"{message}\n");
            new Functions();
            var command = JsonConvert.DeserializeObject<AddPedidoCommand>(message);
            _commandHandler.Handle(command);
        }

        //public static void ProcessUpdateProductCommandQueueMessage([QueueTrigger(UpdateProductCommand.ConstQueueName)] string message, ILogger logger)
        //{
        //    logger.LogInformation($"{message}\n");
        //    new Functions();
        //    var command = JsonConvert.DeserializeObject<UpdateProductCommand>(message);
        //    _commandHandler.Handle(command);
        //}

        //public static void ProcessDeleteProductCommandQueueMessage([QueueTrigger(DeleteProductCommand.ConstQueueName)] string message, ILogger logger)
        //{
        //    logger.LogInformation($"{message}\n");
        //    new Functions();
        //    var command = JsonConvert.DeserializeObject<DeleteProductCommand>(message);
        //    _commandHandler.Handle(command);
        //}
    }
}
