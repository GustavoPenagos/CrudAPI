using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using CrudAPI.Model;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListaController : ControllerBase
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        [HttpGet]
        [Route("/api/ListaAlumnos")]
        public dynamic ListaAlumnos()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM alumnos";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented); ;
                }
            }
            catch (Exception ex)
            {
                return ("Erros", ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/ListaAsignaura")]
        public dynamic ListaAsignaura(string ?strQuery = null)
        {
            try
            {
                string strdata = "";
                string connectionString = configuration.GetConnectionString("colegioDataBase");
                string query = strQuery == null ? "SELECT * FROM asignaturas" : strQuery;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    connection.Close();
                    if(strQuery != null && dataTable != null)
                    { 
                        return dataTable.Rows[0].ItemArray[0].ToString();
                    }
                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                return ("Erros", ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/ListaProfesores")]
        public dynamic ListaProfesores()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM View_Profesores";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented); ;
                }
            }
            catch (Exception ex)
            {
                return ("Erros", ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/ListaClases")]
        public dynamic ListaClases()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM View_Clases";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented); ;
                }

                return ";";
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
