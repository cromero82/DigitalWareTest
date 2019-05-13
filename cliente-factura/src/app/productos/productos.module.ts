import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductosComponent } from './productos.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    ProductosComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
  ],
  exports: [
    ProductosComponent
  ]
})
export class ProductosModule { }
