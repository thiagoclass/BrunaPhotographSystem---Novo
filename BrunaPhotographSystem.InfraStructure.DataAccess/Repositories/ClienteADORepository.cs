//using BrunaPhotographSystem.DomainModel.Entities;
//using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class ClienteADORepository:IClienteRepository
//    {
       

//        public void Create(Cliente entity)
//        {
//            Guid guid = Guid.NewGuid();
//            new LoginADORepository().Create(entity.Login);
//            string SQL = "INSERT INTO Cliente (Id, Nome, email) Values ('"
//                + guid.ToString() + "', '" + entity.Nome.ToString() + "', '" + entity.Login.Username.ToString() + "')";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public void Delete(Guid id)
//        {
//            var username = Read(id).Login.Username;
//            string SQL = "DELETE FROM Cliente WHERE Id = '" + id.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//            new LoginADORepository().Delete(username);
//        }

//        public Cliente Read(Guid id)
//        {
//            Cliente cliente = new Cliente();
//            string SQL = "SELECT * FROM Cliente WHERE id = '" +id.ToString() + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                cliente.Id = Guid.Parse(registro[0]["Id"]);
//                cliente.Nome = registro[0]["Nome"];
//                cliente.Login = new LoginADORepository().Read(registro[0]["email"]);
//            }
//            return cliente;
//        }
//        public IEnumerable<Cliente> ReadAll()
//        {
//            List<Cliente> listCliente = new List<Cliente>();
//            string SQL = "SELECT * FROM Cliente";
//            var registros = BDSQL.ExecutarComandoLeituraSQL(SQL);


//            foreach (var registro in registros)
//            {
//                Cliente cliente = new Cliente();
//                cliente.Id = Guid.Parse(registro["Id"]);
//                cliente.Nome = registro["Nome"];
//                cliente.Login = new LoginADORepository().Read(registro["email"]);

//                listCliente.Add(cliente);
//            }
//            return listCliente;
//        }

//        public void Update(Cliente entity)
//        {
            
//            string SQL = "UPDATE Cliente SET Nome = '" + entity.Nome.ToString() +
//                "', email = '" + entity.Login.Username.ToString() +
//                "' WHERE Id = '" + entity.Id.ToString() + "'";
            
//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
