import {Component, computed, inject, input, OnDestroy, OnInit, signal} from '@angular/core';
import {TaskService} from './task.service';
import {ExecutorComponent} from "./executor/executor.component";
import {FormsModule} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {ExecutorService} from "./executor/executor.service";

@Component({
  selector: 'app-task-card',
  standalone: true,
  imports: [
    ExecutorComponent,
    FormsModule
  ],
  templateUrl: './task-card.component.html',
  styleUrl: './task-card.component.css'
})
export class TaskCardComponent implements OnInit {
  id = input.required<number>();
  title = input.required<string>();
  description = input.required<string>();
  priority = input.required<string>();
  state = input.required<string>();
  deadline = input.required<string>();
  creationFullDate = input.required<string>();
  executors = input.required<any[]>();

  http = inject(HttpClient);
  taskService = inject(TaskService);
  executorService = inject(ExecutorService);
  isEditMode = signal(false);
  priorityColor = signal("black");
  priorityLine = signal("0%");
  currTitle = signal("");
  currDescription = signal("");
  currPriority = signal("");
  currState = signal("");
  currDeadline = signal("");
  currExecutors = signal<any>([]);
  employees : any = [];

  creationDate = computed(() => {
    const i = this.creationFullDate().indexOf(" ");
    return this.creationFullDate().slice(0, i);
  });



  ngOnInit(): void {
    this.priorityColor.set(this.taskService.definePriorityColor(this.priority()));
    this.priorityLine.set(this.taskService.definePriorityLineLength(this.priority()));
    this.currTitle.set(this.title());
    this.currDescription.set(this.description());
    this.currPriority.set(this.priority());
    this.currState.set(this.state());
    this.currDeadline.set(this.deadline());
    this.currExecutors.set(this.executors());
  }

  onUpdatePriority(): void {
    this.priorityColor.set(this.taskService.definePriorityColor(this.currPriority()));
    this.priorityLine.set(this.taskService.definePriorityLineLength(this.currPriority()));
  }

  onUpdateDeadline() {
    this.currDeadline.set(this.currDeadline().replace("-", "."));
    this.currDeadline.set(this.currDeadline().replace("-", "."));
  }

  onSelectNewExecutor(): void {
    this.http.get('https://localhost:5001/api/employee').subscribe({
      next: (res) => {
        this.employees = [];
        let response: any = res;

        for (const executor of response) {
          if (this.currExecutors().some((ex: { id: number; }) => ex.id === executor.id))
            continue;
          this.employees.push(executor);
        }
      },
      error: (err) => {console.log(err)}
    })
  }

  onAddExecutor(event: any): void {
    this.executorService.AddExecutorToList(this.employees, this.currExecutors, event.target.value);
  }

  onEditMode() {
    this.isEditMode.set(true);
    this.currExecutors.set([]);
  }

  onApplyChanges() {
    this.isEditMode.set(false);


    const requestBody = this.taskService.prepareUpdateRequestBody(
      this.id(),
      this.currTitle(),
      this.currDescription(),
      this.currPriority(),
      this.currState(),
      this.currDeadline(),
      this.currExecutors().map((e: { id: number; }) => e.id),
    )

    this.http.put('https://localhost:5001/api/task', requestBody).subscribe({
      next: () => {},
      error: (err) => {console.log(err)}
    });
  }
}
