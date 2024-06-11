import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { tareaModel } from '../Models/tarea.model';

@Injectable({
  providedIn: 'root'
})
export class TareaServiceService {

  url:String = environment.apiBaseUrl;
  listaTarea:tareaModel[] = [];

  //Iniciamos la variable http para poder ocupar peticiones Http
  constructor(private http: HttpClient) { }

  obtenerTareas(){
    this.http.get(this.url+'/obtenerTareas').subscribe({
      next: res => {
        this.listaTarea = res as tareaModel[];
      },
      error: err => { 
        console.log(err);
      }
    });
  }
}
