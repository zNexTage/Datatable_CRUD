using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;
using Datatable.Class;
namespace Datatable.DAL
{
    public class dalUsuario
    {
        public static void CadastrarUsuario(string Nome, string Email, string CPF, string Senha)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DB_DATATABLE"]);
            SqlCommand cmd = new SqlCommand("SPR_CADASTRAR_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@NOME_USUARIO", Nome);
                cmd.Parameters.AddWithValue("@EMAIL_USUARIO", Email);
                cmd.Parameters.AddWithValue("@CPF_USUARIO", CPF);
                cmd.Parameters.AddWithValue("@SENHA_USUARIO", Senha);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }            
        }
        public static List<clsUsuario> SelecionarUsuarios()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DB_DATATABLE"]);
            SqlCommand cmd = new SqlCommand("SPR_SELECIONAR_USUARIOS", con);
            List<clsUsuario> lista = new List<clsUsuario>();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clsUsuario Usuario = new clsUsuario();
                    Usuario.Id = Convert.ToInt32(reader["ID_USUARIO"]);
                    Usuario.Nome = Convert.ToString(reader["NOME_USUARIO"]);
                    Usuario.Email = Convert.ToString(reader["EMAIL_USUARIO"]);
                    Usuario.CPF = Convert.ToString(reader["CPF_USUARIO"]);
                    lista.Add(Usuario);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }

        public static clsUsuario SelecionarPeloId(int Id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DB_DATATABLE"]);
            SqlCommand cmd = new SqlCommand("SPR_SELECIONAR_PELO_ID", con);
            cmd.Parameters.AddWithValue("@ID", Id);         
            cmd.CommandType = CommandType.StoredProcedure;
            clsUsuario obj = new clsUsuario();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {                    
                    obj.Id = Convert.ToInt32(reader["ID_USUARIO"]);
                    obj.Nome = Convert.ToString(reader["NOME_USUARIO"]);
                    obj.Email = Convert.ToString(reader["EMAIL_USUARIO"]);
                    obj.CPF = Convert.ToString(reader["CPF_USUARIO"]);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public static void AtualizarUsuario(int Id, string Nome, string Email, string CPF)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DB_DATATABLE"]);
            SqlCommand cmd = new SqlCommand("SPR_ATUALIZAR_USUARIO", con);
            cmd.Parameters.AddWithValue("@ID", Id);
            cmd.Parameters.AddWithValue("@NOME", Nome);
            cmd.Parameters.AddWithValue("@EMAIL", Email);
            cmd.Parameters.AddWithValue("@CPF", CPF);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            
        }

        public static void DeletarUsuario(int id)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DB_DATATABLE"]);
            SqlCommand cmd = new SqlCommand("SPR_DELETAR_USUARIO", con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
    }
}