namespace ListadoDeTareas.Models
{
    public class Tarea
    {
        public int id_tarea { get; set; }
        public DateTime fecha { get; set; }
        public string nombre { get; set; }
        public int id_prioridad { get; set; }
    }
}
