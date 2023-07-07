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
using CrudAPI.Data;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class ListaController : ControllerBase
    {
        private readonly CrudDbContext? _dbContext;

        public ListaController(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/listar")]
        public dynamic Listar(string name)
        {
            try
            {
                switch (name)
                {
                    case "alumno":
                        List<Alumno> alumnos = (from p in _dbContext.Alumno
                                  select new Alumno
                                  {
                                      Id = p.Id,
                                      Nombre = p.Nombre,
                                      Apellido = p.Apellido,
                                      Edad = p.Edad,
                                      Direccion = p.Direccion,
                                      Telefono = p.Telefono,
                                      Asignatura = p.Asignatura,
                                      Calificacion = p.Calificacion
                                  }).ToList();
                        return JsonConvert.SerializeObject(alumnos, Formatting.Indented);
                    case "profesor":
                        List<Profesor> profesor= (from p in _dbContext.Profesor
                                                select new Profesor
                                                {
                                                    Id = p.Id,
                                                    Nombre = p.Nombre,
                                                    Apellido = p.Apellido,
                                                    Edad = p.Edad,
                                                    Direccion = p.Direccion,
                                                    Telefono = p.Telefono,
                                                    Asignatura = p.Asignatura
                                                }).ToList();
                        return JsonConvert.SerializeObject(profesor, Formatting.Indented);
                    case "asignatura":
                        List<Asignatura> asignaturas= (from p in _dbContext.Asignatura
                                                   select new Asignatura
                                                   {
                                                       Id = p.Id,
                                                       Nombre = p.Nombre,
                                                   }).ToList();
                        return JsonConvert.SerializeObject(asignaturas, Formatting.Indented);
                    default: return BadRequest();
                }
               
               
            }
            catch (Exception ex)
            {
                return ("Erros", ex.Message);
            }
        }
    }
}
