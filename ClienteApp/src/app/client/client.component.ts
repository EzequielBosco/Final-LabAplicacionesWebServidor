import { Component, OnInit } from '@angular/core';
import { ClientService, Client } from '../services/client/client.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-client',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit {
  clients: Client[] = [];

  newClient: Partial<Client> = {
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    address: '',
    dateOfBirth: new Date()
  };

  isLoading = false;
  loadError = '';

  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.isLoading = true;
    this.loadError = '';
    this.clientService.getAllClients().subscribe({
      next: (data) => {
        this.clients = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.loadError = 'No se pudieron cargar los clientes.';
        this.clients = [];
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  onSubmit(): void {
    if (this.newClient.firstName && this.newClient.lastName && this.newClient.email) {
      this.clientService.createClient(this.newClient as Client).subscribe(() => {
        // Reset del formulario
        this.newClient = {
          firstName: '',
          lastName: '',
          email: '',
          phone: '',
          address: '',
          dateOfBirth: new Date()
        };
        this.loadClients();
      });
    }
  }

  deleteClient(id: number): void {
    this.clientService.deleteClient(id).subscribe(() => {
      this.loadClients(); // Refresh list
    });
  }
}
