﻿using ListadoDeTareas.Models;
using System.Threading;

namespace ListadoDeTareas.IServices
{
    public interface TareaInterface
    {
        public List<Tarea> getAllTasks();
        public Response postTask(Tarea tarea);
        public Response deleteTask(int id);
        public Response putTask(int id, Tarea tarea);
    }
}
