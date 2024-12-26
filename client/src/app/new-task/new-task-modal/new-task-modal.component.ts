import {Component, inject, output, signal} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ExecutorComponent} from "../../tasks-grid/task-card/executor/executor.component";
import {ExecutorService} from "../../tasks-grid/task-card/executor/executor.service";
import {NewTaskModalService} from "./new-task-modal.service";
import {TaskService} from "../../tasks-grid/task-card/task.service";

@Component({
  selector: 'app-new-task-modal',
  standalone: true,
  imports: [
    FormsModule,
    ExecutorComponent
  ],
  templateUrl: './new-task-modal.component.html',
  styleUrl: './new-task-modal.component.css'
})
export class NewTaskModalComponent {
  title = signal("");
  description = signal("");
  priority = signal("Low");
  state = signal("Pending");
  deadline = signal("");
  selectedExecutors = signal<any>([]);

  closeModal = output<void>();

  http = inject(HttpClient);
  executorService = inject(ExecutorService);
  newTaskModalService = inject(NewTaskModalService);
  taskService = inject(TaskService);
  employees: any = [];

  onSelectExecutor() {
    this.http.get('https://localhost:5001/api/employee').subscribe({
      next: (res) => {
        this.employees = [];
        let response: any = res;

        for (const executor of response) {
          if (this.selectedExecutors().some((ex: { id: number; }) => ex.id === executor.id))
            continue;
          this.employees.push(executor);
        }
      },
      error: (err) => {console.log(err)}
    })
  }

  onAddExecutor(event: any): void {
    this.executorService.AddExecutorToList(this.employees, this.selectedExecutors, event.target.value);
  }

  onCloseModal(): void {
    this.selectedExecutors.set([]);
    this.title.set("");
    this.description.set("");
    this.deadline.set("");
    this.closeModal.emit()
  }

  onSubmit() {
    const requestBody = this.newTaskModalService.prepareCreateRequestBody(
      this.title(),
      this.description(),
      this.priority(),
      this.state(),
      this.deadline(),
      this.selectedExecutors().map((e: { id: number; }) => e.id)
    );

    console.log(this.deadline());


    this.http.post('https://localhost:5001/api/task', requestBody).subscribe({
      next: (resp) => {this.taskService.tasks.set([...this.taskService.tasks(), resp])},
      error: (err) => {console.log(err)}
    })

    this.onCloseModal()
  }
}
