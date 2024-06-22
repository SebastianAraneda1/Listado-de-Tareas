import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { TareaServiceService } from 'src/app/Core/Services/tarea-service.service';
import { ResponseModel } from 'src/app/Shared/Models/response.model';

@Component({
  selector: 'app-eliminar-tarea',
  templateUrl: './eliminar-tarea.component.html',
  styleUrls: ['./eliminar-tarea.component.scss']
})
export class EliminarTareaComponent {

  constructor(
    public modalService: BsModalService,
    private tareaService: TareaServiceService
  ){}

  cerrarModal(){
    this.modalService.hide();
  }

  eliminarTarea(){
    const id: number = parseInt(sessionStorage.getItem("idEliminar")!);

    this.tareaService.eliminarTarea(id).subscribe({
      next: (response: ResponseModel) => {
        if(response.statusCode == 200){
          alert("Se ha eliminado la tarea!");
          this.modalService.setDismissReason(`eliminado${id}`);
          this.modalService.hide();
          
        }else{
          alert("No se pudo eliminar la tarea");
        }
      },
      error: (err) => {
        console.log(err)
        alert("No se pudo eliminar la tarea");
      }
    });
  }

}
