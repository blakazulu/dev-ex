import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Worker} from '../../../models/worker.model';
import {WorkerHttpService} from '../../../services/http/worker-http.service';
import {Report} from '../../../models/report.model';
import {ReportHttpService} from '../../../services/http/report-http.service';

@Component({
  selector: 'app-manager-details',
  templateUrl: './manager-details.component.html',
  styleUrls: ['./manager-details.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ManagerDetailsComponent implements OnInit {
  manager: Worker | undefined;
  reports: Report[] = [];
  isLoading = false;

  constructor(private route: ActivatedRoute,
              private workerHttpService: WorkerHttpService,
              private reportHttpService: ReportHttpService,
              private cdr: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.setLoadingStatus(true);
        this.getManagerInfo(id);
      }
    });
  }

  getManagerInfo(id: number): void {
    const promises = [
      this.workerHttpService.getManagerById(id),
      this.reportHttpService.getReportsForManager(id)
    ];

    Promise.all(promises)
      .then((result: any[]) => {
        this.manager = result[0];
        this.reports = result[1];
      })
      .finally(() => this.setLoadingStatus(false));
  }

  setLoadingStatus(status: boolean) {
    this.isLoading = status;
    this.cdr.detectChanges();
  }
}
