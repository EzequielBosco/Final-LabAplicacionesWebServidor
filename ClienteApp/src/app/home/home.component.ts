import { Component } from '@angular/core';
import { ButtonComponent } from '../button/button.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ButtonComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  title = 'Home';
  home = false;

  ngOnInit(): void {
    console.log("Componente cargado correctamente!")
  }

  handleClick() {
    this.home = !this.home;
  }
}
