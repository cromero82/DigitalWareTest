import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Productos } from '../model-productos';

@Component({
  selector: 'app-crear-producto',
  templateUrl: './crear-producto.component.html',
  styleUrls: ['./crear-producto.component.scss']
})
export class CrearProductoComponent implements OnInit {
  producto: Productos;
  constructor(
    public activeModal: NgbActiveModal){}

  ngOnInit() {
  }

  closeModal() {
    this.activeModal.close('Modal Closed');
  }
}
