import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ProductType {
  id: number;
  name: string;
  code: string;
  description?: string;
  createdAt: Date;
}

export interface Product {
  id: number;
  name: string;
  code: string;
  stock: number;
  description: string;
  unitPrice: number;
  createdAt: Date;
  productTypeId: number;
  productType: ProductType;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = 'http://localhost:5046/api/products';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  create(product: Partial<Product>): Observable<any> {
    return this.http.post(this.baseUrl, product);
  }

  update(id: number, product: Partial<Product>): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, product);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
