using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using CrudAPI.Model;
using System.Security.Cryptography.Xml;
using Microsoft.Win32;
using CrudAPI.Data;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly CrudDbContext? _dbContext;

        public RegistroController(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("/api/registro")]
        public  dynamic RigistroAlumno(Alumno alumno)
        {
            try
            {
                var insert = new Alumno
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    Apellido = alumno.Apellido,
                    Edad = alumno.Edad,
                    Direccion = alumno.Direccion,
                    Telefono = alumno.Telefono,
                    Asignatura = alumno.Asignatura,
                    Calificacion = alumno.Calificacion
                };

                _dbContext.Alumno.Add(insert);

                _dbContext.SaveChanges();

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
                var insert = new Profesor
                {
                    Id = profesor.Id,
                    Nombre = profesor.Nombre,
                    Apellido = profesor.Apellido,
                    Edad = profesor.Edad,
                    Direccion = profesor.Direccion,
                    Telefono = profesor.Telefono,
                    Asignatura = profesor.Asignatura,
                };

                _dbContext.Profesor.Add(insert);

                _dbContext.SaveChanges();

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
                var insert = new Asignatura
                {
                    Id = asignatura.Id,
                    Nombre = asignatura.Nombre,
                    
                };

                _dbContext.Asignatura.Add(insert);

                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
