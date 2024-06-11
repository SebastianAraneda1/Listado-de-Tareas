using ListadoDeTareas.Models;
using Microsoft.AspNetCore.Mvc;
using ListadoDeTareas.IServices;

namespace ListadoDeTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioridadController : ControllerBase
    {
        private PrioridadInterface prioridadInterface;

        public PrioridadController(PrioridadInterface prioridadInterface)
        {
            this.prioridadInterface = prioridadInterface;
        }

        [HttpGet]
        [Route("/obtenerPrioridades")]
        public ActionResult<List<Prioridad>> getAllPriorities() {
            try
            {
                var prioridades = prioridadInterface.getAllPriorities();
                return Ok(prioridades);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
