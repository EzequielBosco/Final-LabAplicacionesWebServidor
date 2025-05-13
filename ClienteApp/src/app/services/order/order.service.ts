import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface OrderProductItem {
  productId: number;
  quantity: number;
}

export interface OrderCreateRequest {
  clientId: number;
  products: OrderProductItem[];
}

export interface OrderProduct {
  productId: number;
  name: string;
  code: string;
  unitPrice: number;
  quantity: number;
  subTotal: number;
}

export interface Order {
  id: number;
  code: string;
  totalPrice: number;
  dateTime: string;
  clientId: number;
  clientName: string;
  clientCode: string;
  products: OrderProduct[];
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5021/api/orders';

  constructor(private http: HttpClient) { }

  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }

  createOrder(order: OrderCreateRequest): Observable<void> {
    return this.http.post<void>(this.apiUrl, order);
  }
}
