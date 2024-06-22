using ListadoDeTareas.Dal;
using ListadoDeTareas.IServices;
using ListadoDeTareas.Models;
using ListadoDeTareas.Utils;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace ListadoDeTareas.ServiceImpl
{
    public class TareaImpl : TareaInterface
    {
        private DalImpl _dal;
        private ValidacionFeriado utils;

        public TareaImpl(DalImpl dal, ValidacionFeriado utils)
        {
            _dal = dal;
            this.utils = utils;
        }
        public Response getAllTasks()
        {
            Response response = new Response();

            try
            {
                List<Tarea> listaTarea = new List<Tarea>();

                DataTable table = (DataTable)_dal.getDataList("SELECT * FROM Tarea");

                if (table.Rows.Count == 0 || table is null)
                {
                    response.statusCode = 404;
                    response.message = "No se encontraron datos";
                    response.data = null;
                    return response;

                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Tarea tarea = new Tarea();

                    tarea.id_tarea = Convert.ToInt32(table.Rows[i]["id_tarea"]);
                    tarea.fecha = (DateTime)table.Rows[i]["fecha"];
                    tarea.nombre = table.Rows[i]["nombre"].ToString();
                    tarea.id_prioridad = Convert.ToInt32(table.Rows[i]["id_prioridad"]);
                    listaTarea.Add(tarea);
                }

                if (listaTarea.Count == 0)
                {
                    response.statusCode = 404;
                    response.message = "No se encontraron datos";
                    response.data = null;
                    return response;
                }

                response.statusCode = 200;
                response.message = "Exito";
                response.data = listaTarea;
                return response;
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = ex.Message;
                response.data = null;
                return response;
            }
        }

        public Response executeQuery(string query) {
            Response response = new Response();

            try
            {
                int rowAffected = _dal.addUpdateDeleteData(query);

                if (rowAffected == 0)
                {
                    response.statusCode = 500;
                    response.message = "No se encontraron registros";
                    response.data = null;
                    return response;
                }

                response.statusCode = 200;
                response.message = "Exito";
                response.data = null;
                return response;

            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = ex.Message;
                response.data = null;
                return response;
            }
        }


    public bool ValidarFeriado(DateTime fecha)
    {
        try
        {
            string jsonString = utils.ValidarFeriado(fecha).Value.ToString();
            var jsonObject = JObject.Parse(jsonString);

            // Verifica si la propiedad 'error' existe y es true
            if (jsonObject["error"].Value<bool>() == true )
            {
                return true; // La propiedad 'error' existe y es true
            }
            else
            {
                return false; // La propiedad 'error' no existe o no es true
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones
            return false;
        }
    }


    public Response postTask(Tarea tarea)
        {
            if (ValidarFeriado(tarea.fecha))
            {
                string fechaFormateada = tarea.fecha.ToString("yyyy-MM-dd");
                String query = "INSERT INTO Tarea (fecha, nombre, id_prioridad) VALUES ('"
                 + fechaFormateada + "', '" + tarea.nombre + "', " + tarea.id_prioridad + ")";
                return executeQuery(query);
            }

            Response response = new Response();
            response.statusCode = 500;
            response.message = "Dia feriado";
            response.data = null;
            return response;
        }

        public Response deleteTask(int id) {
            String query = "DELETE FROM Tarea WHERE id_tarea =" + id;

            return executeQuery(query);
        }

        public Response putTask(int id, Tarea tarea)
        {
            if (ValidarFeriado(tarea.fecha)) {
                string fechaFormateada = tarea.fecha.ToString("yyyy-MM-dd");
                String query = "UPDATE Tarea SET fecha = '" + fechaFormateada + "', nombre = '"
                    + tarea.nombre + "', id_prioridad = " + tarea.id_prioridad + " WHERE id_tarea = " + id;
                return executeQuery(query);
            }

            Response response = new Response();
            response.statusCode = 500;
            response.message = "Dia feriado";
            response.data = null;
            return response;
        }


    }
}
