using ListadoDeTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListadoDeTareas.IServices
{
    public interface PrioridadInterface
    {
        public List<Prioridad> getAllPriorities();
    }
}
