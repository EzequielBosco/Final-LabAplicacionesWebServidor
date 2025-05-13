import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Client {
  id: number;
  firstName: string;
  lastName: string;
  code: string;
  email: string;
  phone: string;
  address: string;
  dateOfBirth: Date;
}

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private baseUrl = 'http://localhost:5282/api/clients';

  constructor(private http: HttpClient) { }

  getAllClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.baseUrl);
  }

  getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.baseUrl}/${id}`);
  }

  createClient(client: Client): Observable<any> {
    return this.http.post(this.baseUrl, client);
  }

  updateClient(id: number, client: Client): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, client);
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
