import { Component, OnInit } from '@angular/core';
import { TareaServiceService } from 'src/app/Services/tarea-service.service';

@Component({
  selector: 'app-lista-tareas',
  templateUrl: './lista-tareas.component.html',
  styleUrls: ['./lista-tareas.component.scss']
})
export class ListaTareasComponent implements OnInit {

  constructor(public tareaService: TareaServiceService){}

  ngOnInit(): void {
    this.tareaService.obtenerTareas();
  }
}
