using CrudAPI.Data;
using CrudAPI.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class BuscarController : ControllerBase
    {
        private readonly CrudDbContext? _dbContext;

        public BuscarController(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/buscar")]
        public dynamic Buscar(string Id, string? asg, string name)
        {
            try
            {
                switch (name)
                {
                    case "alumno":
                        if(asg == null)
                        {
                            return JsonConvert.SerializeObject(_dbContext.Alumno.Where(a => a.Id == Id).ToList(), Formatting.Indented);
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(_dbContext.Alumno.Where(a => a.Id == Id && a.Asignatura == asg).ToList(), Formatting.Indented);
                        }                        
                    case "asignatura":
                        return JsonConvert.SerializeObject(_dbContext.Asignatura.Where(a => a.Id == Id).ToList(), Formatting.Indented);
                    
                    case "profesor":
                        return JsonConvert.SerializeObject(_dbContext.Profesor.Where(a => a.Id == Id).ToList(), Formatting.Indented);
                    default: return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }        
    }
}
