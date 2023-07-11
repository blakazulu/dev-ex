import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {WorkerHttpService} from '../../services/http/worker-http.service';
import {Worker} from '../../models/worker.model';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeesComponent implements OnInit {
  employees: Worker[] = [];
  isLoading = false;

  constructor(private workerHttpService: WorkerHttpService,
              private cdr: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.showEmployees();
  }

  showEmployees() {
    this.setLoadingStatus(true);
    this.workerHttpService.getAllWorkers()
      .then((employees) => {
        this.employees = employees;
      }).finally(() => {
      this.setLoadingStatus(false);
    });
  }

  setLoadingStatus(status: boolean) {
    this.isLoading = status;
    this.cdr.detectChanges();
  }
}
