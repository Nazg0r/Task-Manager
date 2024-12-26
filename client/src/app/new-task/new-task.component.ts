import {Component, signal} from '@angular/core';
import {NewTaskModalComponent} from "./new-task-modal/new-task-modal.component";

@Component({
  selector: 'app-new-task',
  standalone: true,
  imports: [
    NewTaskModalComponent
  ],
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.css'
})
export class NewTaskComponent {
  isModalOpen = signal<boolean>(true);

  onModalClosed() {
    this.isModalOpen.set(true);
  }

  onModalOpened() {
    this.isModalOpen.set(false);
  }
}
