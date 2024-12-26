export interface Task {
  id: number;
  title: string;
  description: string;
  priority: string;
  state: string;
  timeToFinish: string;
  creationDate: string;
  employees: Employee[];
}

export interface Employee {
  id: number;
  name: string;
  surname: string;
  workLoad: string;
  taskIds: number[];
}
