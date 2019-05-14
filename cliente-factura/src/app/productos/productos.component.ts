import { CrearProductoComponent } from './crear-producto/crear-producto.component';
import { ProductosService } from './productos.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Productos } from './model-productos';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
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
    dataRows: Productos[];
    optionsModal: NgbModalOptions = {windowClass: 'modal-opened', centered: true};
    constructor (
      private servicio: ProductosService,
      private activatedRoute: ActivatedRoute,
      private modalService: NgbModal
     ){}

    ngOnInit(){

      this.activatedRoute.paramMap.subscribe(params => {
        this.servicio.list().subscribe( (items:any) => {
          this.dataRows = items.Data as Productos[];
        }, error =>{
          alert('Error consultando datos');
        }
        );
      });
    }
    editar(item:Productos){
      const modalRef = this.modalService.open(CrearProductoComponent, this.optionsModal);

      modalRef.result.then((result) => {
        debugger;
        console.log(result);
      }).catch((error) => {
        console.log(error);
      });
    }
}
