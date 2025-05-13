import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService, Product } from '../services/product/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  productId!: number;
  product: Partial<Product> = {};
  productTypeId: number = 1;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.productId = +this.route.snapshot.paramMap.get('id')!;
    this.productService.getById(this.productId).subscribe({
      next: (data) => {
        this.product = data;
        this.productTypeId = data.productType?.id ?? 1;
      },
      error: () => alert('No se pudo cargar el producto')
    });
  }

  updateProduct(): void {
    const updatePayload = {
      ...this.product,
      productTypeId: this.productTypeId
    };

    this.productService.update(this.productId, updatePayload).subscribe(() => {
      this.router.navigate(['/products']);
    });
  }
}
