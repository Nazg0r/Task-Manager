import {Component, inject, signal} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {TaskService} from "../tasks-grid/task-card/task.service";

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent {
  keyword = signal("");
  priority = signal("Low");
  deadline = signal("");
  executorId = signal(0);

  employees: any = [];
  http = inject(HttpClient);
  taskService = inject(TaskService);

  onSetDeadline() {
    this.deadline.set(this.deadline().replace("-", "."));
    this.deadline.set(this.deadline().replace("-", "."));
  }

  onDropExecutors() {
    this.http.get('https://localhost:5001/api/employee').subscribe({
      next: (res) => {this.employees = res},
      error: (err) => {console.log(err)}
    })
  }

  onSelectExecutor(event: any) {
    if (event.target.value === "Any") {
      this.executorId.set(0);
      return;
    }

    const selectedExecutor = this.employees.find((e: { name: string; surname: string; }) => {
      const fullName = `${e.name} ${e.surname}`;
      return fullName === event.target.value;
    });

    this.executorId.set(selectedExecutor.id);
  }

  onFilterSubmit() {
    let priority = this.priority();
    if (this.priority() === "Any") priority = "";

    let requestAddress = `https://localhost:5001/api/task` +
      `?priority=${priority}` +
      `&titleKeyWord=${this.keyword()}` +
      `&timeToFinish=${this.deadline()}` +
      `&employerId=${this.executorId()}`;

    this.http.get(requestAddress).subscribe({
        next: (response) => {this.taskService.tasks.set(response);},
        error: (err) => {console.log(err)}
      }
    )
  }
}
