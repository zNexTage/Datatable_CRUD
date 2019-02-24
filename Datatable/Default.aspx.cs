using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datatable.DAL;
using Datatable.Class;
namespace Datatable
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void CadastrarUsuario(string Nome, string Email, string CPF, string Senha)
        {
            dalUsuario.CadastrarUsuario(Nome, Email, CPF, Senha);
        }
        [WebMethod]
        public static List<clsUsuario> SelecionarUsuarios()
        {
            return dalUsuario.SelecionarUsuarios();
        }
        [WebMethod]
        public static clsUsuario SelecionarPeloId(int Id)
        {
            return dalUsuario.SelecionarPeloId(Id);
        }
        [WebMethod]
        public static void AtualizarUsuario(int Id, string Nome, string Email, string CPF)
        {
            dalUsuario.AtualizarUsuario(Id, Nome, Email, CPF);
        }
        [WebMethod]
        public static void DeletarUsuario(int Id)
        {
            dalUsuario.DeletarUsuario(Id);
        }
    }
}