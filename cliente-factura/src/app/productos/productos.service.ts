import { Productos } from './model-productos';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {map, catchError, tap} from 'rxjs/operators'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

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

  create (equipo: Productos): Observable<Productos> {
    return this.http.post<Productos>(this.resourceUrl, equipo );
  }

  update (equipo: Productos): Observable<Productos> {
    return this.http.put<Productos>(this.resourceUrl, equipo, httpOptions );
  }

  detail (equipoId: number): Observable<Productos> {
    return this.http.get<Productos>(this.resourceUrl + '/' + equipoId);
  }

  delete (equipoId: number): Observable<Productos> {
    return this.http.delete<Productos>(this.resourceUrl + '/' + equipoId);
  }
}
