import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TareaModel } from '../../Shared/Models/tarea.model';
import { ResponseModel } from 'src/app/Shared/Models/response.model';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TareaServiceService {

  url: String = environment.apiBaseUrl;
  listaTarea: TareaModel[] = [];
  formData: TareaModel = new TareaModel();

  //Iniciamos la variable http para poder ocupar peticiones Http
  constructor(private http: HttpClient) { }


  //Se crea el servicio para obtener todas las tareas
  obtenerTareas(): Observable<ResponseModel> {
    //Se mapean los datos
    return this.http.get<ResponseModel>(`${this.url}/obtenerTareas`);
  }

  editarTarea(tarea: TareaModel): Observable<ResponseModel> {
    return this.http.put<ResponseModel>(`${this.url}/editarTarea/${tarea.id_tarea}`, tarea);
  }

  eliminarTarea(id: number): Observable<ResponseModel>{
    return this.http.delete<ResponseModel>(`${this.url}/eliminarTarea/${id}`);
  } 

  agregarTarea(tarea: TareaModel): Observable<ResponseModel>{
    return this.http.post<ResponseModel>(`${this.url}/agregarTarea`, tarea);
  }
}
