import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { PrioridadServiceService } from 'src/app/Core/Services/prioridad-service.service';
import { TareaServiceService } from 'src/app/Core/Services/tarea-service.service';
import { PrioridadModel } from 'src/app/Shared/Models/prioridad.model';
import { ResponseModel } from 'src/app/Shared/Models/response.model';
import { TareaModel } from 'src/app/Shared/Models/tarea.model';

@Component({
  selector: 'app-form-editar',
  templateUrl: './form-editar.component.html',
  styleUrls: ['./form-editar.component.scss']
})
export class FormEditarComponent implements OnInit{

  listaPrioridades: Array<PrioridadModel> = [];
  fechaTarea: string = "";
  tareaEditar: TareaModel = new TareaModel();
  
  constructor(
    private prioridadService: PrioridadServiceService,
    public modalService: BsModalService,
    private datePipe: DatePipe,
    private tareaService: TareaServiceService
  ){}

  ngOnInit(): void {
    this.obtenerPrioridades();
    //aqui se recupera el valor guardado del sesion storage
    this.tareaEditar = JSON.parse(sessionStorage.getItem('tarea')!);
    console.log(this.tareaEditar);
    this.fechaTarea = this.datePipe.transform(this.tareaEditar.fecha, 'yyyy-MM-dd')!;
    console.log("fechaTarea", this.fechaTarea);
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

  editarTarea(): void{
    console.log(this.fechaTarea);
    this.tareaEditar.fecha = new Date(this.fechaTarea);
    this.tareaService.editarTarea(this.tareaEditar).subscribe({
      next: (response: ResponseModel) => {
        if(response.statusCode == 200){
          alert("Tarea actualizada!");
          //La razon del cierre del modal es que se edita, y se convierte el objeto que se edito a un JSON pero en String
          this.modalService.setDismissReason(`editado${JSON.stringify(this.tareaEditar)}`);
          this.modalService.hide();
        }
        else{
          alert(response.message);
        }
      },
      error: (err) => {
        alert("Ha ocurrido un error");
        console.log(err);
      }
    });
    
  }
}
