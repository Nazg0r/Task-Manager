import {Component, inject, OnInit} from '@angular/core';
import {TaskCardComponent} from "./task-card/task-card.component";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-tasks-grid',
  standalone: true,
  imports: [
    TaskCardComponent
  ],
  templateUrl: './tasks-grid.component.html',
  styleUrl: './tasks-grid.component.css'
})
export class TasksGridComponent implements OnInit {
  http = inject(HttpClient);
  tasks: any;

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/task').subscribe({
      next: (response) => {this.tasks = response;},
      error: (err) => {console.log(err)}
      }
    )
  }
}
