using ListadoDeTareas.Dal;
using ListadoDeTareas.IServices;
using ListadoDeTareas.Models;
using System.Data;
using System.Threading;

namespace ListadoDeTareas.ServiceImpl
{
    public class TareaImpl : TareaInterface
    {
        private DalImpl _dal;

        public TareaImpl(DalImpl dal)
        {
            _dal = dal;
        }
        public List<Tarea> getAllTasks()
        {
            List<Tarea> listaTarea = new List<Tarea>();

            DataTable table = (DataTable)_dal.getDataList("SELECT * FROM Tarea");

            if (table.Rows.Count == 0 || table is null)
            {
                throw new Exception("No se encontraron datos");

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
                throw new Exception("No se encontraron datos");
            }

            return listaTarea;
          
        }

        public Response executeQuery(string query) {
            Response response = new Response();

            try
            {
                int rowAffected = _dal.addUpdateDeleteData(query);

                if (rowAffected == 0)
                {
                    response.statusCode = 500;
                    response.errorMessage = "No se encontraron registros";
                    response.data = null;
                    return response;
                }

                response.statusCode = 200;
                response.errorMessage = "Exito";
                response.data = null;
                return response;

            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.errorMessage = ex.Message;
                response.data = null;
                return response;
            }
        }

        public Response postTask(Tarea tarea)
        {
            String query = "INSERT INTO Tarea (fecha, nombre, id_prioridad) VALUES ('"
             + tarea.fecha + "', '" + tarea.nombre + "', " + tarea.id_prioridad + ")";
            return executeQuery(query);
        }

        public Response deleteTask(int id) {
            String query = "DELETE FROM Tarea WHERE id_tarea =" + id;

            return executeQuery(query);
        }

        public Response putTask(int id, Tarea tarea)
        {
            String query = "UPDATE Tarea SET fecha = '" + tarea.fecha + "', nombre = '" 
                + tarea.nombre + "', id_prioridad = " + tarea.id_prioridad + " WHERE id_tarea = " + id;
            return executeQuery(query);
        }


    }
}
