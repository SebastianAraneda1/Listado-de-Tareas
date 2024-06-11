using ListadoDeTareas.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ListadoDeTareas.Dal
{
    public class DalImpl
    {
        public IConfiguration _configuration;

        public DalImpl(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public Object getDataList(String query) {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ListadoDeTareasCon")))
            {

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                DataTable table = new DataTable();

                dataAdapter.Fill(table);

                List<Object> listaPrioridad = new List<Object>();

                if (table.Rows.Count == 0)
                {
                    return null;
                }

                return table;

            }
        }

        public int addUpdateDeleteData(string query)
        {
            try
            {
                int rowsAffected = 0;

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ListadoDeTareasCon")))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }

                return rowsAffected;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


    }
}
