using Microsoft.AspNetCore.Mvc;
using WebApplication7.Core.Core.Uow;
using WebApplication7.Models;

#pragma warning disable
namespace WebApplication7.Controllers
{
    public class HomeController: Controller
    {
        private TesteServiceUow service { get; set; }

        public HomeController(TesteServiceUow _service)
        #region MyRegion
        {
            this.service = _service;
        } 
        #endregion

        [HttpPost("/listar")]
        public List<AgendaEntity> listar()
        #region MyRegion
        {
            return this.service.getLista();
        }
        #endregion


        [HttpPost("/adicionar")]
        public IActionResult adicionar([FromBody] ResponseListaContatos param)
        #region MyRegion
        {
            this.service.setParam(param);
            this.service.setLista();
            return Ok("Adicionado");
        }
        #endregion
    }
}
