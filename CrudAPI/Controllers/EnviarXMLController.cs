using CrudAPI.Data;
using CrudAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class EnviarXMLController : ControllerBase
    {
        private readonly CrudDbContext? _dbContext;

        public EnviarXMLController(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("api/XML")]
        public async Task<dynamic> EnviarXML()
        {
            try
            {
                DataTable tablaSalida = new DataTable();
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    using (var command = new SqlCommand("SP_Compras_Impuestos", (SqlConnection)connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros XML
                        var xmlUsuarios = new SqlParameter("@xml_usuarios", SqlDbType.Xml);
                        xmlUsuarios.Value = "<Data><Usuario><Id>14</Id><Nombre>Juan</Nombre></Usuario><Usuario><Id>17</Id><Nombre>Maria</Nombre></Usuario><Usuario><Id>25</Id><Nombre>Carlos</Nombre></Usuario><Usuario><Id>15</Id><Nombre>Fernanda</Nombre></Usuario></Data>";
                        command.Parameters.Add(xmlUsuarios);

                        var xmlCompras = new SqlParameter("@xml_compras", SqlDbType.Xml);
                        xmlCompras.Value = "<Data><Item><Usuario>14</Usuario><Producto>78</Producto><Valor>300</Valor></Item><Item><Usuario>17</Usuario><Producto>23</Producto><Valor>568</Valor></Item><Item><Usuario>17</Usuario><Producto>99</Producto><Valor>350</Valor></Item><Item><Usuario>14</Usuario><Producto>99</Producto><Valor>107</Valor></Item><Item><Usuario>25</Usuario><Producto>23</Producto><Valor>425</Valor></Item></Data>";
                        command.Parameters.Add(xmlCompras);

                        var xmlItemsIva = new SqlParameter("@xml_itemsIva", SqlDbType.Xml);
                        xmlItemsIva.Value = "<Data><Producto><IdProducto>23</IdProducto><Porcentaje>0.16</Porcentaje></Producto><Producto><IdProducto>99</IdProducto><Porcentaje>0.19</Porcentaje></Producto></Data>";
                        command.Parameters.Add(xmlItemsIva);

                        connection.Open();
                        //Letura de salida
                        using (var reader = command.ExecuteReader())
                        {
                            tablaSalida.Load(reader);
                        }
                    }
                }

                return JsonConvert.SerializeObject(tablaSalida);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
