using CrudAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class ActualizarController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ActualizarController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        [Route("api/actualizar/alumnos")]
        public dynamic ActualizarAlumno(Alumnos alumno)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using(SqlCommand cmd = new SqlCommand("SP_Actualizar_Alumno", con, tran))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Id_Alumno", alumno.Id_Alumno);
                            cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", alumno.Apellido);
                            cmd.Parameters.AddWithValue("@Edad", alumno.Edad);
                            cmd.Parameters.AddWithValue("@Direccion", alumno.Direccion);
                            cmd.Parameters.AddWithValue("@Telefono", alumno.Telefono);
                            cmd.Parameters.AddWithValue("@Asignatura", alumno.Asignatura);
                            cmd.Parameters.AddWithValue("@Calificacion", alumno.Calificacion);

                            tran.Commit();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                        finally
                        {
                            tran?.Dispose();
                        }
                    }
                }
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /*
        [HttpPost]
        [Route("api/actualizar/profesor")]
        public dynamic ActualizarPofesor(Alumnos alumno)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("SP_Actualizar_Alumno", con, tran))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Id_Alumno", alumno.Id_Alumno);
                            cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", alumno.Apellido);
                            cmd.Parameters.AddWithValue("@Edad", alumno.Edad);
                            cmd.Parameters.AddWithValue("@Direccion", alumno.Direccion);
                            cmd.Parameters.AddWithValue("@Telefono", alumno.Telefono);
                            cmd.Parameters.AddWithValue("@Asignatura", alumno.Asignatura);
                            cmd.Parameters.AddWithValue("@Calificacion", alumno.Calificacion);

                            tran.Commit();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                        finally
                        {
                            tran?.Dispose();
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        */

        [HttpGet]
        [Route("api/eliminar/alumno")]
        public dynamic EliminarAlumno(string Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    con.Open();
                    string query = "delete from Alumno where Id_Alumno = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
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
