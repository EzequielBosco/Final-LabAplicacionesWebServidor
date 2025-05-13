import { Component, OnInit } from '@angular/core';
import { ProductService, Product } from '../services/product/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Product[] = [];

  newProduct: Partial<Product> = {
    name: '',
    code: '',
    stock: 1,
    unitPrice: 1,
    description: '',
    productTypeId: 1
  };

  isLoading = false;
  loadError = '';

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.loadError = '';
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.loadError = 'No se pudieron cargar los productos.';
        this.products = [];
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  onSubmit(): void {
    this.productService.create(this.newProduct).subscribe(() => {
      this.newProduct = { name: '', code: '', stock: 0, productTypeId: 1 };
      this.loadProducts();
    });
  }

  deleteProduct(id: number): void {
    this.productService.delete(id).subscribe(() => {
      this.loadProducts();
    });
  }
}
