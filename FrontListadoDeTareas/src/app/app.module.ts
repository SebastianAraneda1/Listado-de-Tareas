import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TitularComponent } from './Components/titular/titular.component';
import { ListaTareasComponent } from './Components/lista-tareas/lista-tareas.component';
import { HttpClientModule } from '@angular/common/http';
import { FormAgregarComponent } from './Components/form-agregar/form-agregar.component';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormEditarComponent } from './Components/form-editar/form-editar.component';
import { EliminarTareaComponent } from './Components/eliminar-tarea/eliminar-tarea.component';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    TitularComponent,
    ListaTareasComponent,
    FormAgregarComponent,
    FormEditarComponent,
    EliminarTareaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot()
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
