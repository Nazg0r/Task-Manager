import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class TaskService {
  definePriorityColor(priority: string) {
    if (priority === 'Low') return 'green';
    if (priority === 'Medium') return 'orange';
    if (priority === 'High') return 'red';
    return 'black';
  }

  definePriorityLineLength(priority: string) {
    if (priority === 'Low') return '33%';
    if (priority === 'Medium') return '66%';
    if (priority === 'High') return '100%';
    return '0%';
  }

  prepareUpdateRequestBody(currId: number, newTitle: string, newDescription: string, newPriority: string, newState: string, newDeadline: string, newEmployeeIds: number[]) {
    return {
      id : currId,
      title : newTitle,
      description : newDescription,
      priority : newPriority,
      state : newState,
      timeToFinish : newDeadline,
      employeeIds : newEmployeeIds
    };
  }
}
