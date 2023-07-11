import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {WorkerHttpService} from '../../services/http/worker-http.service';
import {Worker} from '../../models/worker.model';

@Component({
  selector: 'app-managers',
  templateUrl: './managers.component.html',
  styleUrls: ['./managers.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ManagersComponent implements OnInit {
  managers: Worker[] = [];
  isLoading = false;

  constructor(private workerHttpService: WorkerHttpService,
              private cdr: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.showEmployees();
  }

  showEmployees() {
    this.setLoadingStatus(true);
    this.workerHttpService.getAllManagers()
      .then((managers) => {
        this.managers = managers;
      }).finally(() => {
      this.setLoadingStatus(false);
    });
  }

  setLoadingStatus(status: boolean) {
    this.isLoading = status;
    this.cdr.detectChanges();
  }
}
