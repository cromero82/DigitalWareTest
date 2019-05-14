import { CrearProductoModule } from './crear-producto/crear-producto.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductosComponent } from './productos.component';
import { HttpClientModule } from '@angular/common/http';
import { CrearProductoComponent } from './crear-producto/crear-producto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ProductosComponent,
    CrearProductoComponent
  ],
  imports: [
    CommonModule,
    CrearProductoModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    ProductosComponent,
    //CrearProductoComponent
  ],
  entryComponents: [CrearProductoComponent],
})
export class ProductosModule { }
