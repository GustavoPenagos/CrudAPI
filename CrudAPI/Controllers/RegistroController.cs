using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using CrudAPI.Model;
using System.Security.Cryptography.Xml;
using Microsoft.Win32;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class RegistroController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public RegistroController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        [Route("/api/registro/alumnos")]
        public  dynamic RigistroAlumno(Alumnos alumno)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using(SqlCommand cmd = new SqlCommand("SP_Insertar_Alumno", con, tran))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Id_Alumno", alumno.Id_Alumno);
                            cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", alumno.Apellido == null ? "" : alumno.Apellido);
                            cmd.Parameters.AddWithValue("@Edad", alumno.Edad);
                            cmd.Parameters.AddWithValue("@Direccion", alumno.Direccion);
                            cmd.Parameters.AddWithValue("@Telefono", alumno.Telefono == null ? "" : alumno.Telefono);
                            cmd.Parameters.AddWithValue("@Asignatura", alumno.Asignatura == null ? "" : alumno.Asignatura);
                            cmd.Parameters.AddWithValue("@Calificacion", alumno.Calificacion == null ? "" : alumno.Calificacion);

                            tran.Commit();
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception ex)
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

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/registro/profesores")]
        public dynamic RigistroProfesor(Profesor profesor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("SP_Insertar_Profesor", con, tran))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Id_Profesor", profesor.Id_Profesor);
                            cmd.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", profesor.Apellido);
                            cmd.Parameters.AddWithValue("@Edad", profesor.Edad);
                            cmd.Parameters.AddWithValue("@Direccion", profesor.Direccion);
                            cmd.Parameters.AddWithValue("@Telefono", profesor.Telefono);
                            cmd.Parameters.AddWithValue("@Asignatura", profesor.Asignatura);

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
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/registro/asignaturas")]
        public dynamic RigistroAsignatura(Asignatura asignatura)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("SP_Insertar_Asignatura", con, tran))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Codigo", asignatura.Codigo);
                            cmd.Parameters.AddWithValue("@Nombre", asignatura.Nombre);

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
                return BadRequest();
            }
        }


    }
}
