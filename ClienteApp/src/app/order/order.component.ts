import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  OrderService,
  OrderCreateRequest,
  Order
} from '../services/order/order.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orders: Order[] = [];
  newOrder: OrderCreateRequest = {
    clientId: 1,
    products: [
      { productId: 1, quantity: 1 }
    ]
  };

  isLoading = false;
  loadError = '';

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.isLoading = true;
    this.loadError = '';
    this.orderService.getAllOrders().subscribe({
      next: (data) => {
        this.orders = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.loadError = 'No se pudieron cargar las ordenes.';
        this.orders = [];
        this.isLoading = false;
        console.error('Error cargando órdenes', err);
      }
    });
  }

  addProduct(): void {
    this.newOrder.products.push({ productId: 1, quantity: 1 });
  }

  deleteProduct(index: number): void {
    this.newOrder.products.splice(index, 1);
  }

  onSubmit(): void {
    this.orderService.createOrder(this.newOrder).subscribe(() => {
      this.newOrder = { clientId: 0, products: [] };
      this.loadOrders(); // Refresh list
    });
  }
}
