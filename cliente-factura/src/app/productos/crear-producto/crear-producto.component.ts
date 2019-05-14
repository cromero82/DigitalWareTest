import { ProductosService } from './../productos.service';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Productos } from '../model-productos';
// import { FormGroup, FormBuilder } from '@angular/forms';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-crear-producto',
  templateUrl: './crear-producto.component.html',
  styleUrls: ['./crear-producto.component.scss']
})
export class CrearProductoComponent implements OnInit {
  producto: Productos;
  @Input()id: number;
  myForm: FormGroup;
  constructor(
    private servicio: ProductosService,
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder
    ){
      this.createForm();
    }

    private createForm() {
      this.myForm = this.formBuilder.group({
        Codigo: '',
        Nombre: '',
        Valor: ''
      });
    }

  ngOnInit() {
  }

  closeModal() {
    this.activeModal.close('Modal Closed');
  }

  private submitForm() {
    debugger;
    this.producto = this.myForm.value;
    this.servicio.create(this.producto).subscribe(
      data => {
        alert("ok");
        this.activeModal.close(data);
        // this.snackBar.open('¡Se actualizaron los datos con exito!', 'X', {
        //   duration: 5000,
        //   panelClass: ['success-snackbar']
        // });
        // const urlBack = location.pathname.replace(location.pathname.split('/')[location.pathname.split('/').length - 1], 'admin');
        // this.router.navigate([urlBack]);
      },
      error => {
        alert("error: ")
        this.activeModal.close(error);
        // this.disableSubmit = false;
        // this.snackBar.open('Se presento un problema con el servidor, por favor comuníquese con el administrador', 'X', {
        //   duration: 10000,
        //   panelClass: ['error-snackbar']
        // });
      }
    );
  }
}
