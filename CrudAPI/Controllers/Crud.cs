using CrudAPI.Data;
using CrudAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class Crud : ControllerBase
    {
        private readonly CrudDbContext? _dbContext;

        public Crud(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("api/actualizar/alumno")]
        public dynamic ActualizarAlumno(Alumno alumno, string id, string asg)
        {
            try
            {
               var entityAlm = _dbContext.Alumno.FirstOrDefault(a => a.Id == id && a.Asignatura == asg);
                
                if (entityAlm != null)
                {
                    entityAlm.Nombre = alumno.Nombre;
                    entityAlm.Apellido = alumno.Apellido;
                    entityAlm.Edad = alumno.Edad;
                    entityAlm.Direccion = alumno.Direccion;
                    entityAlm.Telefono = alumno.Telefono;
                    entityAlm.Asignatura = alumno.Asignatura;
                    entityAlm.Calificacion = alumno.Calificacion;

                    _dbContext.SaveChanges();                       
                }
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        
        [HttpPost]
        [Route("api/actualizar/profesor")]
        public dynamic ActualizarPofesor(Profesor profesor, string id)
        {
            try
            {
                var update = _dbContext.Profesor.FirstOrDefault(a => a.Id == id);

                if (update != null)
                {
                    update.Nombre = profesor.Nombre;
                    update.Apellido = profesor.Apellido;
                    update.Edad = profesor.Edad;
                    update.Direccion = profesor.Direccion;
                    update.Telefono = profesor.Telefono;
                    update.Asignatura = profesor.Asignatura;

                    _dbContext.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/actualizar/asignatura")]
        public dynamic ActualizarAsignatura(Asignatura asignatura, string id)
        {
            try
            {
                var update = _dbContext.Asignatura.FirstOrDefault(a => a.Id == id);

                if (update != null)
                {
                    update.Nombre = asignatura.Nombre;

                    _dbContext.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("api/eliminar")]
        public dynamic Eliminar(string id, string? asg=null, string? name=null)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configBuilder.GetConnectionString("colegioDataBase");
            string deleteQuery = "";
            try
            {
                switch (name)
                {
                    case "alumno":
                        var borrar = _dbContext.Alumno.FirstOrDefault(a => a.Id == id);
                        if (!borrar.Asignatura.Equals("")){ return BadRequest(); } 
                        deleteQuery = "DELETE FROM " + name + " WHERE id = '" + id + "' and asignatura = '" + asg + "'";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                            {
                                command.ExecuteNonQuery();
                               
                            }

                            connection.Close();
                        }
                        break;
                    case "profesor":
                        deleteQuery = "DELETE FROM " + name + " WHERE id = '" + id ;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                            {
                                command.ExecuteNonQuery();

                            }

                            connection.Close();
                        }
                        break;
                    case "asignatura":
                        deleteQuery = "DELETE FROM " + name + " WHERE id = '" + id;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                            {
                                command.ExecuteNonQuery();

                            }

                            connection.Close();
                        }
                        break;
                    default: return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
