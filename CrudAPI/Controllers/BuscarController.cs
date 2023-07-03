using CrudAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class BuscarController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public BuscarController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("api/buscar")]
        public dynamic Buscar(string Id, string? asg, string name)
        {
            try
            {
                List<Alumnos> list = new List<Alumnos>();
                string query = "";
                if (asg == null)
                {
                    query = "select * from " + name + " where Id_Alumno = '" + Id + "'";
                }
                else
                {
                    query = "select * from " + name + " where Id_Alumno = '" + Id + "' and Asignatura = '" + asg + "'";
                }

                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Alumnos alumno = new Alumnos()
                                    {
                                        Id_Alumno = reader.GetString(0),
                                        Nombre = reader.GetString(1),
                                        Apellido = reader.GetString(2),
                                        Edad = reader.GetString(3),
                                        Direccion = reader.GetString(4),
                                        Telefono = reader.GetString(5),
                                        Asignatura = reader.GetString(6),
                                        Calificacion = reader.GetString(7)
                                    };

                                    list.Add(alumno);
                                }
                            }
                            
                        }
                        return JsonConvert.SerializeObject(list, Formatting.Indented);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
