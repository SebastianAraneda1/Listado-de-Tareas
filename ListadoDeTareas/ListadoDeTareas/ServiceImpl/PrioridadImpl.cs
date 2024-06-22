using ListadoDeTareas.IServices;
using ListadoDeTareas.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using ListadoDeTareas.Dal;

namespace ListadoDeTareas.ServiceImpl
{
    public class PrioridadImpl : PrioridadInterface
    {
        private DalImpl _dal;

        public PrioridadImpl(DalImpl dal)
        {
            _dal = dal;
        }
        public Response getAllPriorities()
        {
            Response response = new Response();
            try
            {

                List<Prioridad> listaPrioridad = new List<Prioridad>();

                DataTable table = (DataTable) _dal.getDataList("SELECT * FROM Prioridad");

                if (table.Rows.Count == 0 || table is null)
                {
                    response.statusCode = 404;
                    response.message = "No se encontraron datos";
                    response.data = null;
                    return response;

                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Prioridad prioridad = new Prioridad();

                    prioridad.id_prioridad = Convert.ToInt32(table.Rows[i]["id_prioridad"]);
                    prioridad.descripcion = table.Rows[i]["descripcion"].ToString();
                    listaPrioridad.Add(prioridad);
                }

                if (listaPrioridad.Count == 0)
                {
                    response.statusCode = 404;
                    response.message = "No se encontraron datos";
                    response.data = null;
                    return response;
                }

                response.statusCode = 200;
                response.message = "Exito";
                response.data = listaPrioridad;
                return response;
            }
            catch (Exception ex) {
                response.statusCode = 500;
                response.message = ex.Message;
                response.data = null;
                return response;
            }
        }
    }
}
