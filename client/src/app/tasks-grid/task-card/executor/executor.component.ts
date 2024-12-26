import {Component, computed, inject, input, OnInit, signal} from '@angular/core';
import {ExecutorService} from "./executor.service";

@Component({
  selector: 'app-executor',
  standalone: true,
  imports: [],
  templateUrl: './executor.component.html',
  styleUrl: './executor.component.css'
})

export class ExecutorComponent implements OnInit {

  name = input.required<string>();
  surname = input.required<string>();
  executorService = inject(ExecutorService);
  blockColor = signal("black");
  fullName = computed(() => this.name() + " " + this.surname())

  ngOnInit(): void {
    this.blockColor.set(this.executorService.generateColor(this.fullName()));
  }
}
