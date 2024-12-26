import {Component, inject} from '@angular/core';
import { BurgerMenuComponent } from "./burger-menu/burger-menu.component";
import {HttpClient} from "@angular/common/http";
import {TaskService} from "../tasks-grid/task-card/task.service";

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
  http = inject(HttpClient);
  taskService = inject(TaskService);

  onBurgerMenuOpen() {
    this.burgerMenu = !this.burgerMenu;
  }

  onAllClicked() {
    this.http.get('https://localhost:5001/api/task').subscribe({
      next: (response) => {this.taskService.tasks.set(response);},
      error: (err) => {console.log(err)}
      }
    )
  }

  onPendingClicked() {
    this.http.get('https://localhost:5001/api/task?state=pending').subscribe({
        next: (response) => {this.taskService.tasks.set(response);},
        error: (err) => {console.log(err)}
      }
    )
  }

  onProgressClicked() {
    this.http.get('https://localhost:5001/api/task?state=In%20Progress').subscribe({
        next: (response) => {this.taskService.tasks.set(response);},
        error: (err) => {console.log(err)}
      }
    )
  }

  onFinishedClicked() {
    this.http.get('https://localhost:5001/api/task?state=Finished').subscribe({
        next: (response) => {this.taskService.tasks.set(response);},
        error: (err) => {console.log(err)}
      }
    )
  }
}
