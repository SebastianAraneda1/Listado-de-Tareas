import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { PrioridadModel } from 'src/app/Shared/Models/prioridad.model';
import { ResponseModel } from 'src/app/Shared/Models/response.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PrioridadServiceService {

  url: String = environment.apiBaseUrl;

  public listaPrioridades: PrioridadModel[] = [];

  constructor(private http: HttpClient) { }

  //Se crea un servicio para hacer una llamada a la API C#
  obtenerPrioridades(): Observable<ResponseModel>{
    return this.http.get<ResponseModel>(`${this.url}/obtenerPrioridades`);
  }

}
