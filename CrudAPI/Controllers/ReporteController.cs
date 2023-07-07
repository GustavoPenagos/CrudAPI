using CrudAPI.Data;
using CrudAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly CrudDbContext? _dbContext;

        public ReporteController(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/reporte")]
        public dynamic Reporte()
        {
            List<Reporte> reporteList = new List<Reporte>();
            try
            {
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ExportToResport";
                        command.Connection = connection;
                        
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                Reporte reporte = new Reporte
                                {
                                    Id_Alumno = reader["Id_Alumno"].ToString(),
                                    Nombre_Alumno = reader["Nombre_Alumno"].ToString(),
                                    Codigo_Materia = reader["Codigo_Materia"].ToString(),
                                    Nombre_Materia = reader["Nombre_Materia"].ToString(),
                                    Id_Profesor = reader["Id_Profesor"].ToString(),
                                    Nombre_Profesor = reader["Nombre_Profesor"].ToString(),
                                    Calificaicon_Final = Convert.ToDouble(reader["Calificaicon_Final"].ToString()),
                                };
                                reporteList.Add(reporte);
                            }
                        }
                    }
                }
                return JsonConvert.SerializeObject(reporteList); 
                
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
