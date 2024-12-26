import {Injectable} from '@angular/core';

@Injectable({ providedIn: 'root' })
export class NewTaskModalService {
  prepareCreateRequestBody(taskTitle: string, taskDescription: string, taskPriority: string, taskState: string, taskDeadline: string, taskEmployeeIds: number[]) {
    return {
      title : taskTitle,
      description : taskDescription,
      priority : taskPriority,
      state : taskState,
      timeToFinish : taskDeadline,
      employeeIds : taskEmployeeIds
    };
  }
}
