import { Component, OnInit } from '@angular/core';
import { PrioridadServiceService } from 'src/app/Core/Services/prioridad-service.service';
import { TareaServiceService } from 'src/app/Core/Services/tarea-service.service';
import { PrioridadModel } from 'src/app/Shared/Models/prioridad.model';
import { ResponseModel } from 'src/app/Shared/Models/response.model';
import { TareaModel } from 'src/app/Shared/Models/tarea.model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormEditarComponent } from '../form-editar/form-editar.component';
import { EliminarTareaComponent } from '../eliminar-tarea/eliminar-tarea.component';
import { FormAgregarComponent } from '../form-agregar/form-agregar.component';

@Component({
  selector: 'app-lista-tareas',
  templateUrl: './lista-tareas.component.html',
  styleUrls: ['./lista-tareas.component.scss']
})
export class ListaTareasComponent implements OnInit {

  public listaTareas: Array<TareaModel> = [];

  public listaPrioridades: Array<PrioridadModel> = [];

  modalRef!: BsModalRef;

  constructor(
    private tareaService: TareaServiceService,
    private prioridadService: PrioridadServiceService,
    public modalService:BsModalService
  ){}

  ngOnInit(): void {

    this.obtenerPrioridades();

  }

  obtenerPrioridades(): void{
    this.prioridadService.obtenerPrioridades().subscribe({
      next: (response: ResponseModel) => {

        if(response.statusCode == 200){
          this.listaPrioridades = response.data;
          this.obtenerTareas();
        }
      },error: (error) => {
        console.error('Error al obtener las prioridades:', error);
      }
    })
  }

  //En este metodo se hace el llamado al servicio de obtener tareas del tareas Service, se le pasa una variable response
  //La variable response servira para obtener el estado del codigo como validación y tambien para obtener el data
  obtenerTareas(): void{
    this.tareaService.obtenerTareas().subscribe({
      next: (response: ResponseModel) => {

        if(response.statusCode == 200){

          this.listaTareas = response.data;
          //Adicionalmente se llamará al metodo de obtener descripción de tareas para poder
          this.obtenerDescripcionPrioridad(this.listaTareas);

        }
      },error: (error) => {
        console.error('Error al obtener las tareas:', error);
      }
    })
  }

  obtenerDescripcionPrioridad(arregloTareas: Array<TareaModel>): void{
    //Se recorren las tareas para obtener el nombre de la prioridad
    arregloTareas.forEach(tarea => {
      //Dentro de la lista de prioridades se pide que se encuentre a la prioridad que tenga el id que tiene la tarea recorrida donde al final la tarea.nombre_prioridad sera igual a la descripción que encuentre a continuación
      tarea.nombre_prioridad = this.listaPrioridades.find(
        //Con una variable Prioridad, se crea una function donde se recorrera la lista de prioridad hasta que la tarea.id_prioridad sea igual a la prioridad.id_prioridad para que retorne la descripción
        prioridad => tarea.id_prioridad === prioridad.id_prioridad
        //Despues de esto se obtiene la descripcion indicando con el ! que siempre va a retornar algo
      )!.descripcion;
    });

  }

  editarTarea(tarea: TareaModel){

    //Aqui se guarda sesión de la tarea  en el navegador con el sesion storage
    sessionStorage.setItem("tarea", JSON.stringify(tarea));
    this.modalRef = this.modalService.show(FormEditarComponent);

    //aqui se suscribe a la razon del cierre, es decir, se espera que el modal cierre y conocer la razon
    this.modalRef.onHidden!.subscribe((reason: string | any) => {

      //Se valida que la razón del cierre incluya palabra "editado", siempre incluira editado siempre y cuando el resultado del servicio sea 200
      if(reason.includes('editado')){

        //si incluye la palabra editado se elimina la palabra para obtener el String del objeto JSON y se vuelve a convertir en objeto con JSON parse
        //En tareaEditada se obtiene los datos que se envian a actualizar.
        const tareaEditada: TareaModel = JSON.parse(reason.replace('editado', ''));   

          //De la tareaeditada, el nombre de su prioridad se seteara el valor desde la lista de prioridades donde coincidan los ID, debe coincidir el ID de la prioridad y el de la tareaEditada.id_prioridad
          tareaEditada.nombre_prioridad = this.listaPrioridades.find(prioridad => prioridad.id_prioridad == tareaEditada.id_prioridad)!.descripcion;

          //El .map es un metodo que se utiliza para recorrer un arreglo cuando se desea su modificacion  
          this.listaTareas = this.listaTareas.map(tarea => {

            //Se valida que coincidan los ID de la tarea editada y de la lista de tarea
            if (tarea.id_tarea === tareaEditada.id_tarea) {

              // Si el id de la tarea coincide con el de la tarea actualizada, retorna la tarea actualizada.
              return tareaEditada;
            } else {

              // De lo contrario, retorna la tarea sin cambios.
              return tarea;
            }
        });
      }
    });
  }

  //Este metodo abrirá el modal
  eliminarTarea(id: number){
    //se abre el modal
    sessionStorage.setItem("idEliminar", JSON.stringify(id));
    this.modalRef = this.modalService.show(EliminarTareaComponent);
    //Aqui se suscribe a un evento onHidden, es decir cuando el modal se cierre...
    this.modalRef.onHidden!.subscribe((reason: string | any) => {
      //Se valida que la razón del cierre incluya palabra "eliminado"
      if(reason.includes('eliminado')){
        //si incluye la palabra eliminado se elimina la palabra para obtener el id
        const id = parseInt(reason.replace('eliminado', ''));
        //Se busca el indice de ese ID dentro de la lista de tareas
        let index = this.listaTareas.findIndex(tarea => tarea.id_tarea === id);
        //Se valida que el índice sea mayor a -1, es decir significara que existe en la lista.
        if (index !== -1) {
          //una vez encontrado se elimina de la lista, le pasa el indice y borra el registro
          this.listaTareas.splice(index, 1);
        }

      }
    });
  }

  AgregarTarea() {
    this.modalRef = this.modalService.show(FormAgregarComponent);

    this.modalRef.onHidden!.subscribe((reason: string | any) => {

      //Se valida que la razón del cierre incluya palabra "creado", siempre incluira editado siempre y cuando el resultado del servicio sea 200
      if(reason.includes('creado')){

        //si incluye la palabra creado se elimina la palabra para obtener el String del objeto JSON y se vuelve a convertir en objeto con JSON parse
        //En tareaEditada se obtiene los datos que se envian a actualizar.
        const tareaCreada: TareaModel = JSON.parse(reason.replace('creado', ''));   

          //De la tareaCreada, el nombre de su prioridad se seteara el valor desde la lista de prioridades donde coincidan los ID, debe coincidir el ID de la prioridad y el de la tareaEditada.id_prioridad       
          tareaCreada.nombre_prioridad = 
          this.listaPrioridades.find(prioridad => prioridad.id_prioridad == tareaCreada.id_prioridad)!.descripcion;
          
          if(this.listaTareas.length > 0){
            //Va a buscar la ultima tarea de la lista, toma su id y suma 1
            tareaCreada.id_tarea = this.listaTareas[this.listaTareas.length-1].id_tarea+1;
          }
          else{
            tareaCreada.id_tarea = 1;
          }


          //Aqui se añade a la lista de tareas
          this.listaTareas.push(tareaCreada);

      }
    });

   }

}
