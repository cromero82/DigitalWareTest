import { ProductosService } from './productos.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
// declare interface ProductoData {
//     headerRow: string[];
//     dataRows: string[][];
// }

@Component({
    selector: 'productos-cmp',
    moduleId: module.id,
    templateUrl: 'productos.component.html'
})

export class ProductosComponent implements OnInit{
    headerRow: [ 'Id', 'Codigo', 'Nombre', 'Valor', 'Estregistro'];
    dataRows: [];

    constructor (private servicio: ProductosService, private activatedRoute: ActivatedRoute){}

    ngOnInit(){

      this.activatedRoute.paramMap.subscribe(params => {
        this.servicio.list().subscribe( (items:any) => {
          this.dataRows = items.Data;
        }, error =>{
          alert('Error consultando datos');
        }
        );
      });
    }
}
