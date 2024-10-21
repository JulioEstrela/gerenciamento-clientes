using Crud.Data;
using Crud.Models.Entities;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crud.Controllers
{
    public class EnderecoController : Controller
    {
        #region Injeção de Dependência - DB
        private readonly ApplicationDbContext dbContext;

        public EnderecoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion



        // Create
        [HttpPost]
        public IActionResult PostEndereco(string values)
        {
            if (values == null)
            {
                return BadRequest(ModelState.GetFullErrorMessage());
            }
            var enderecoRecebido = JsonConvert.DeserializeObject<dynamic>(values);
            Endereco.TipoEndereco tipoEndereco = (Endereco.TipoEndereco) enderecoRecebido.Tipo;


            Endereco endereco = null;

            switch (tipoEndereco)
            {
                case Endereco.TipoEndereco.Fiscal:
                    endereco = new EnderecoFiscal();
                    break;
                case Endereco.TipoEndereco.Cobranca:
                    endereco = new EnderecoCobranca();
                    break;
                case Endereco.TipoEndereco.Entrega:
                    endereco = new EnderecoEntrega();
                    break;
            }


            JsonConvert.PopulateObject(values, endereco);
            var cliente = dbContext.Clientes.FirstOrDefault(c => c.Id == endereco.ClienteId);

            if (!TryValidateModel(endereco))
                return BadRequest(ModelState.GetFullErrorMessage());
            if (cliente.Enderecos == null)
                cliente.Enderecos = new List<Endereco>();


            dbContext.Enderecos.Add(endereco);
            dbContext.SaveChanges();

            cliente.Enderecos.Add(endereco);

            return Ok(endereco);
        }

        // Read
        [HttpGet]
        public object GetEndereco(Guid id, int tipo, DataSourceLoadOptions loadOptions)
        {
            var enderecos = new List<Endereco>();
            dbContext.Enderecos.Where(endereco => endereco.ClienteId == id && endereco.Tipo == (Endereco.TipoEndereco) tipo).ToList().ForEach(enderecos.Add);
            return DataSourceLoader.Load(enderecos, loadOptions);
        }

        // Update
        [HttpPut]
        public IActionResult PutEndereco(Guid key, string values)
        {
            var endereco = dbContext.Enderecos.FirstOrDefault(e => e.Id == key);
            JsonConvert.PopulateObject(values, endereco);

            if (!TryValidateModel(endereco))
                return BadRequest(ModelState.GetFullErrorMessage());

            dbContext.SaveChanges();

            return Ok(endereco);
        }

        // Delete
        [HttpDelete]
        public void DeleteEndereco(Guid key)
        {
            var endereco = dbContext.Enderecos.FirstOrDefault(e => e.Id == key);
            dbContext.Enderecos.Remove(endereco);
            dbContext.SaveChanges();
        }
    }
}
