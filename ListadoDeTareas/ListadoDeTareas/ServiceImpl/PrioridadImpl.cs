using ListadoDeTareas.IServices;
using ListadoDeTareas.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using ListadoDeTareas.Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ListadoDeTareas.ServiceImpl
{
    public class PrioridadImpl : PrioridadInterface
    {
        private DalImpl _dal;

        public PrioridadImpl(DalImpl dal)
        {
            _dal = dal;
        }
        public List<Prioridad> getAllPriorities()
        {
            
            List<Prioridad> listaPrioridad = new List<Prioridad>();

            DataTable table = (DataTable) _dal.getDataList("SELECT * FROM Prioridad");

            if (table.Rows.Count == 0 || table is null)
            {
                throw new Exception("No se encontraron datos");
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
                throw new Exception("No se encontraron datos");
            }

            return listaPrioridad;
        }
    }
}
