//using BrunaPhotographSystem.DomainModel.ValueObjects;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BrunaPhotographSystem.InfraStructure.DataAccess.Repositories
//{
//    public class LoginADORepository
//    {   
//        public void Create(Login entity)
//        {
//            Guid guid = Guid.NewGuid();
//            string SQL = "INSERT INTO Login (Username, Password) Values ('"
//                + entity.Username.ToString() + "', '" + entity.Password.ToString() + "')";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public void Delete(String username)
//        {
//            string SQL = "DELETE FROM Login WHERE Username = '" + username.ToString() + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }

//        public Login Read(String username)
//        {
//            Login login = new Login();
//            string SQL = "SELECT * FROM Login WHERE Username = '" + username + "'";

//            var registro = BDSQL.ExecutarComandoLeituraSQL(SQL);
//            if (registro.Count > 0)
//            {
//                login.Username = (registro[0]["Username"]);
//                login.Password = (registro[0]["Password"]);
//                login.Administrador = Boolean.Parse(registro[0]["Administrador"]);
//            }
//            return login;
//        }
        

//        public void Update(Login login)
//        {
//            string SQL = "UPDATE Login SET Password = '" + login.Password.ToString() +
//                "' WHERE Username = '" + login.Username + "'";

//            BDSQL.ExecutarComandoSQL(SQL);
//        }
//    }
//}
