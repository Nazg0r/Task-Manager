import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from "./header/header.component";
import {TasksGridComponent} from "./tasks-grid/tasks-grid.component";
import {NewTaskComponent} from "./new-task/new-task.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TasksGridComponent, HeaderComponent, NewTaskComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'client';
}
