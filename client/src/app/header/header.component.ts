import { Component } from '@angular/core';
import { BurgerMenuComponent } from "./burger-menu/burger-menu.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    BurgerMenuComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  burgerMenu: boolean = false;

  onBurgerMenuOpen() {
    this.burgerMenu = !this.burgerMenu;
  }
}
