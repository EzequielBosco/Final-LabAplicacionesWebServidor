import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ClientService, Client } from '../services/client/client.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-client-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './client-edit.component.html',
  styleUrls: ['./client-edit.component.css']
})
export class ClientEditComponent implements OnInit {
  clientId!: number;
  client: Partial<Client> = {};
  dateOfBirthString: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private clientService: ClientService
  ) { }

  ngOnInit(): void {
    this.clientId = +this.route.snapshot.paramMap.get('id')!;
    this.clientService.getClientById(this.clientId).subscribe({
      next: (data) => {
        this.client = data;
        this.dateOfBirthString = new Date(data.dateOfBirth).toISOString().split('T')[0]
      },
      error: () => alert('No se pudo cargar el cliente')
    });
  }

  updateClient(): void {
    this.client.dateOfBirth = new Date(this.dateOfBirthString);
    this.clientService.updateClient(this.clientId, this.client as Client).subscribe(() => {
      this.router.navigate(['/clients']);
    });
  }
}
