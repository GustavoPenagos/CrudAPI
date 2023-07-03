using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using CrudAPI.Model;
using Microsoft.Extensions.Configuration;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class ListaController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ListaController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("/api/lista/alumnos")]
        public dynamic ListaAlumnos()
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM alumno";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                return ("Erros", ex.Message);
            }
        }

        [HttpGet]
        [Route("api/lista/profesores")]
        public dynamic ListaProfesores()
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("colegioDataBase");
                string query = "SELECT * FROM Profesor";
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
        [Route("api/lista/asignaturas")]
        public dynamic ListaAsignaura(string ?strQuery = null)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("colegioDataBase");
                string query = strQuery == null ? "SELECT * FROM Asignatura" : strQuery;
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

        

    }
}
