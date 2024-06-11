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
        public Response getAllPriorities() {
            return prioridadInterface.getAllPriorities();
        }
    }
}
