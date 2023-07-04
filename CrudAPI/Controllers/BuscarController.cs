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
                List<Alumnos> listAlmno = new List<Alumnos>();
                List<Profesor> listProfesor = new List<Profesor>();
                List<Asignatura> listAsignatura = new List<Asignatura>();
                string query = ""; 
                if (asg == null)
                {
                    query = "select * from " + name + " where Id  = '" + Id + "'";
                }
                else
                {
                    query = "select * from " + name + " where Id  = '" + Id + "' and Asignatura = '" + asg + "'";
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
                                switch (name)
                                {
                                    case "Alumno":
                                        while (reader.Read())
                                        {

                                            Alumnos alumno = new Alumnos()
                                            {
                                                Id = reader.GetString(0),
                                                Nombre = reader.GetString(1),
                                                Apellido = reader.GetString(2),
                                                Edad = reader.GetString(3),
                                                Direccion = reader.GetString(4),
                                                Telefono = reader.GetString(5),
                                                Asignatura = reader.GetString(6),
                                                Calificacion = reader.GetString(7)

                                            };
                                            listAlmno.Add(alumno);
                                        }
                                        return JsonConvert.SerializeObject(listAlmno, Formatting.Indented);

                                    case "Profesor":
                                        while (reader.Read())
                                        {
                                            Profesor profesor = new Profesor()
                                            {
                                                Id = reader.GetString(0),
                                                Nombre = reader.GetString(1),
                                                Apellido = reader.GetString(2),
                                                Edad = reader.GetString(3),
                                                Direccion = reader.GetString(4),
                                                Telefono = reader.GetString(5),
                                                Asignatura = reader.GetString(6),
                                            };
                                            listProfesor.Add(profesor);
                                        }
                                        return JsonConvert.SerializeObject(listProfesor, Formatting.Indented);

                                    case "Asignatura":

                                        while (reader.Read())
                                        {
                                            Asignatura asignatura = new Asignatura()
                                            {
                                                Id = reader.GetString(0),
                                                Nombre = reader.GetString(1)
                                            };
                                            listAsignatura.Add(asignatura);
                                        }
                                        return JsonConvert.SerializeObject(listAsignatura, Formatting.Indented);
                                }                                
                            }
                            return reader.HasRows.ToString();
                        }
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
