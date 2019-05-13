import { Productos } from './model-productos';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {map, catchError, tap} from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class ProductosService {
  private resourceUrl: string;
  constructor(
    private http: HttpClient,
  ) {
    this.resourceUrl = 'http://localhost:55512/api/Productos';
  }

  list (): Observable<Productos[]> {
    return this.http.get<Productos[]>(this.resourceUrl);
  }
}
