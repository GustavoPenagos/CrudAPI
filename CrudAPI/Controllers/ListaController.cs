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
                        return JsonConvert.SerializeObject(_dbContext.Alumno.ToList(), Formatting.Indented);
                    case "profesor":
                        return JsonConvert.SerializeObject(_dbContext.Profesor.ToList(), Formatting.Indented);
                    case "asignatura":
                        return JsonConvert.SerializeObject(_dbContext.Asignatura.ToList(), Formatting.Indented);
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
