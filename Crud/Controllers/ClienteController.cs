using Crud.Data;
using Crud.Models.Entities;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crud.Controllers
{
    public class ClienteController : Controller
    {
        #region Injeção de Dependência - DB
        private readonly ApplicationDbContext dbContext;

        public ClienteController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        // View - Listar Clientes
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        // Create
        [HttpPost]
        public IActionResult PostCliente(string values)
        {
            var cliente = new Cliente();
            JsonConvert.PopulateObject(values, cliente);

            if (!TryValidateModel(cliente))
                return BadRequest(ModelState.GetFullErrorMessage());

            dbContext.Clientes.Add(cliente);
            dbContext.SaveChanges();

            return Ok(cliente);
        }

        // Read
        [HttpGet]
        public object GetCliente(DataSourceLoadOptions loadOptions)
        {
            var clientes = dbContext.Clientes.ToList();
            return DataSourceLoader.Load(clientes, loadOptions);
        }

        // Update
        [HttpPut]
        public IActionResult PutCliente(Guid key, string values)
        {
            var cliente = dbContext.Clientes.FirstOrDefault(c => c.Id == key);
            JsonConvert.PopulateObject(values, cliente);

            if (!TryValidateModel(cliente))
                return BadRequest(ModelState.GetFullErrorMessage());

            dbContext.SaveChanges();

            return Ok(cliente);
        }

        // Delete
        [HttpDelete]
        public void DeleteCliente(Guid key)
        {
            var cliente = dbContext.Clientes.FirstOrDefault(c => c.Id == key);
            dbContext.Clientes.Remove(cliente);
            dbContext.SaveChanges();
        }
    }
}
