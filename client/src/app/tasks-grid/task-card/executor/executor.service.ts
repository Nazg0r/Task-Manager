import {Injectable, WritableSignal} from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ExecutorService {
  generateColor(seed: string): string {
    let hash = 0;
    for (let i = 0; i < seed.length; i++) {
      hash = seed.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = "#";
    for (let i = 0; i < 3; i++) {
      const value = (hash >> (i * 8)) & 0xff;
      color += ("00" + value.toString(16)).slice(-2);
    }

    return color;
  }

  AddExecutorToList(allExecutors: any[], executorsList: WritableSignal<any>, fullExecutorName: string) {
    const newExecutor = allExecutors.find((e: { name: string; surname: string; }) => {
      const fullName = `${e.name} ${e.surname}`;
      return fullName === fullExecutorName;
    });

    executorsList.set([...executorsList(), newExecutor]);
  }
}
