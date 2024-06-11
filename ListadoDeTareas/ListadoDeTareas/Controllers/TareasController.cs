using Microsoft.AspNetCore.Mvc;
using ListadoDeTareas.Models;
using ListadoDeTareas.IServices;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ListadoDeTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private TareaInterface tareaInterface;
        public TareasController(TareaInterface _tareaInterface) {
            tareaInterface = _tareaInterface;
        }
        // GET: api/<TareasController>
        [HttpGet]
        [Route("/obtenerTareas")]
        public Response getAllTasks()
        {
            return tareaInterface.getAllTasks();
        }

        // POST api/<TareasController>
        [HttpPost]
        [Route("/agregarTarea")]
        public Response postTask([FromBody] Tarea tarea)
        {   
            return tareaInterface.postTask(tarea);
        }

        //PUT api/<TareasController>/5
        [HttpPut("/editarTarea/{id}")]
        public Response putTask(int id, [FromBody] Tarea tarea)
        {
            return tareaInterface.putTask(id, tarea);
        }

        // DELETE api/<TareasController>/5
        [HttpDelete("/eliminarTarea/{id}")]
        public Response deleteTask(int id)
        {
            return tareaInterface.deleteTask(id);
        }
    }
}
