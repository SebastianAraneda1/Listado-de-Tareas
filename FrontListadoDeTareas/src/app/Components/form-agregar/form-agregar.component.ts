import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { PrioridadServiceService } from 'src/app/Core/Services/prioridad-service.service';
import { TareaServiceService } from 'src/app/Core/Services/tarea-service.service';
import { PrioridadModel } from 'src/app/Shared/Models/prioridad.model';
import { ResponseModel } from 'src/app/Shared/Models/response.model';
import { TareaModel } from 'src/app/Shared/Models/tarea.model';

@Component({
  selector: 'app-form-agregar',
  templateUrl: './form-agregar.component.html',
  styleUrls: ['./form-agregar.component.scss']
})
export class FormAgregarComponent implements OnInit{

  listaPrioridades: Array<PrioridadModel> = [];

  nuevaTarea: TareaModel = new TareaModel();
  
  fechaTarea: string = "";

  constructor(
    private prioridadService: PrioridadServiceService,
    public modalService: BsModalService,
    private tareaService: TareaServiceService
  ){}

  ngOnInit(): void {
    this.obtenerPrioridades();
  }

  obtenerPrioridades(): void{
    this.prioridadService.obtenerPrioridades().subscribe({
      next: (response: ResponseModel) => {

        if(response.statusCode == 200){
          this.listaPrioridades = response.data;
        }
      },error: (error) => {
        console.error('Error al obtener las prioridades:', error);
      }
    })
  }


  cerrarModal(){
    this.modalService.hide();
  }

  agregarTarea(){
    //Se setea la fecha de la neuva tarea en fechaTarea, porque en el datePicker hay que tratarla como String para mostrar la fecha
    this.nuevaTarea.fecha = new Date(this.fechaTarea);
    this.tareaService.agregarTarea(this.nuevaTarea).subscribe({
      next: (res: ResponseModel) => {
        if(res.statusCode === 200){
          alert('Tarea agregada correctamente');
          //Razon de cierre para agregar
          this.modalService.setDismissReason(`creado${JSON.stringify(this.nuevaTarea)}`);
          this.modalService.hide();
        }
        else{
          alert(res.message);
        }

      },
      error: (err) => {
        alert("No se pudo agregar la tarea");
        console.log(err);
      }
    });
  }

}
