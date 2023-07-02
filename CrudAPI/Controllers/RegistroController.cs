using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using CrudAPI.Model;
using System.Security.Cryptography.Xml;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistroController : ControllerBase
    {
        public RegistroController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        [Route("/api/RigistroAlumno")]
        public  dynamic RigistroAlumno(Alumnos alumno)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SP_Insertar_Alumno";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter parametro1 = new SqlParameter("@Identificacion", SqlDbType.VarChar);
                    parametro1.Value = alumno.Identificacion;
                    cmd.Parameters.Add(parametro1);

                    SqlParameter parametro2 = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametro2.Value = alumno.Nombre;
                    cmd.Parameters.Add(parametro2);

                    SqlParameter parametro3 = new SqlParameter("@Apellido", SqlDbType.VarChar);
                    parametro3.Value = alumno.Apellido;
                    cmd.Parameters.Add(parametro3);

                    SqlParameter parametro4 = new SqlParameter("@Edad", SqlDbType.Int);
                    parametro4.Value = alumno.Edad;
                    cmd.Parameters.Add(parametro4);

                    SqlParameter parametro5 = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    parametro5.Value = alumno.Direccion;
                    cmd.Parameters.Add(parametro5);

                    SqlParameter parametro6 = new SqlParameter("@Telefono", SqlDbType.VarChar);
                    parametro6.Value = alumno.Telefono;
                    cmd.Parameters.Add(parametro6);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                ListaController lista = new ListaController();
                return new
                {
                    success = true,
                    messagge = "Exitoso",
                    result = new
                    {
                        Usuario = JsonConvert.SerializeObject(alumno)
                    }
                };
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/RigistroProfesor")]
        public dynamic RigistroProfesor(Profesores profesor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SP_Insertar_Profesor";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter parametro1 = new SqlParameter("@Identificacion", SqlDbType.VarChar);
                    parametro1.Value = profesor.Identificacion;
                    cmd.Parameters.Add(parametro1);

                    SqlParameter parametro2 = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametro2.Value = profesor.Nombre;
                    cmd.Parameters.Add(parametro2);

                    SqlParameter parametro3 = new SqlParameter("@Apellido", SqlDbType.VarChar);
                    parametro3.Value = profesor.Apellido;
                    cmd.Parameters.Add(parametro3);

                    SqlParameter parametro4 = new SqlParameter("@Edad", SqlDbType.Int);
                    parametro4.Value = profesor.Edad;
                    cmd.Parameters.Add(parametro4);

                    SqlParameter parametro5 = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    parametro5.Value = profesor.Direccion;
                    cmd.Parameters.Add(parametro5);

                    SqlParameter parametro6 = new SqlParameter("@Telefono", SqlDbType.VarChar);
                    parametro6.Value = profesor.Telefono;
                    cmd.Parameters.Add(parametro6);

                    SqlParameter parametro7 = new SqlParameter("@Cog_Asignatura", SqlDbType.Int);
                    parametro7.Value = profesor.Cod_Asignatura;
                    cmd.Parameters.Add(parametro7);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                
                return new
                {
                    success = true,
                    messagge = "Exitoso",
                    result = new
                    {
                        Usuario = JsonConvert.SerializeObject(profesor)
                    }
                };
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/RigistroAsignatura")]
        public dynamic RigistroAsignatura(Asignaturas asignatura)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SP_Insedrtar_Asignatura";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter parametro1 = new SqlParameter("@Codigo", SqlDbType.Int);
                    parametro1.Value = asignatura.Codigo;
                    cmd.Parameters.Add(parametro1);

                    SqlParameter parametro2 = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametro2.Value = asignatura.Nombre;
                    cmd.Parameters.Add(parametro2);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                
                return new
                {
                    success = true,
                    messagge = "Exitoso",
                    result = new
                    {
                        Usuario = JsonConvert.SerializeObject(asignatura)
                    }
                };
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/RegistroFinal")]
        public dynamic RegistroFinal(string id, int asignatura, string estado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SP_Insertar_Califiacion";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter parametro1 = new SqlParameter("@IdentificacionAlumno", SqlDbType.VarChar);
                    parametro1.Value = id;
                    cmd.Parameters.Add(parametro1);

                    SqlParameter parametro2 = new SqlParameter("@Asignatura", SqlDbType.Int);
                    parametro2.Value = asignatura;
                    cmd.Parameters.Add(parametro2);

                    SqlParameter parametro3 = new SqlParameter("@Aprobado", SqlDbType.VarChar);
                    parametro3.Value = estado;
                    cmd.Parameters.Add(parametro3);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                }
                ListaController lista = new ListaController();
                var materia = lista.ListaAsignaura("SELECT Nombre FROM asignaturas where Codigo = " + asignatura);
                if (estado.Equals("no", StringComparison.OrdinalIgnoreCase))
                {
                    estado = "Materia no aprobada";
                }
                else
                {
                    estado = "Materia aprobado";
                }
                return new
                {
                    success = true,
                    messagge = "Exitoso",
                    result = new
                    {
                        IdentificacionAlumno = id,
                        Materia = materia,
                        Estado = estado

                    }
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    messagge = "Error",
                    result = new
                    {
                        error = ex.Message
                    }
                };
            }
                
                
            
        }

        [HttpPost]
        [Route("/api/RegistroClases")]
        public dynamic RegistroClases(Clases clases)
        {
            try
            {
                if (!ValidarClase(clases))
                {
                    return BadRequest();
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandText = "SP_InsertarClases";
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter parametro1 = new SqlParameter("@Id_Alumno", SqlDbType.VarChar);
                        parametro1.Value = clases.Id_Alumno;
                        cmd.Parameters.Add(parametro1);

                        SqlParameter parametro2 = new SqlParameter("@Id_Profesor", SqlDbType.Int);
                        parametro2.Value = clases.Id_Profesor;
                        cmd.Parameters.Add(parametro2);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                    }
                    return new
                    {
                        success = true,
                        messagge = "Exitoso",
                        result = new
                        {
                            Usuario = JsonConvert.SerializeObject(clases)
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/Validar")]
        public dynamic ValidarClase(Clases clases)
        {
            try
            {
                string IdA = clases.Id_Alumno;
                string IdP = clases.Id_Profesor;
                List<Clases> clasesList = new List<Clases>();

                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("colegioDataBase")))
                {
                    connection.Open();
                    string query = "select * from Clases";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Clases clas = new Clases();
                            clas.Id_Alumno = row["Id_Alumno"].ToString();
                            clas.Id_Profesor = row["Id_Profesor"].ToString();

                            clasesList.Add(clas);
                        }
                        for (int i = 0; i < clasesList.Count; i++)
                        {
                            if (clasesList[i].Id_Alumno.Equals(IdA) && clasesList[i].Id_Profesor.Equals(IdP))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
