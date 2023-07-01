using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
            catch(Exception ex)
            {
                return("Erros",ex.Message);
            }
        }
        
        [HttpGet]
        [Route("/api/ListaAsignaura")]
        public dynamic ListaAsignaura()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM asignaturas";
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
        [Route("/api/ListaProfesores")]
        public dynamic ListaProfesores()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM profesores";
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
        [Route("/api/ListaFinal")]
        public dynamic ListaFinal()
        {
            try
            {

            }catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
