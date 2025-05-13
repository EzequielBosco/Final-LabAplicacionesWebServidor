import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ClientComponent } from './client/client.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ClientDetailComponent } from './client-detail/client-detail.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ClientEditComponent } from './client-edit/client-edit.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'clients', component: ClientComponent },
  { path: 'clients/:id', component: ClientDetailComponent },
  { path: 'clients/:id/edit', component: ClientEditComponent },
  { path: 'products', component: ProductComponent },
  { path: 'products/:id', component: ProductDetailComponent },
  { path: 'products/:id/edit', component: ProductEditComponent },
  { path: 'orders', component: OrderComponent },
  { path: 'usuarios', redirectTo: '/clients', pathMatch: 'full' },
  { path: 'productos', redirectTo: '/products', pathMatch: 'full' },
  { path: 'ordenes', redirectTo: '/orders', pathMatch: 'full' }
];
