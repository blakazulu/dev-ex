import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {WorkerHttpService} from '../../../services/http/worker-http.service';
import {Worker} from '../../../models/worker.model';
import {Task} from '../../../models/task.model';
import {TaskHttpService} from '../../../services/http/task-http.service';
import {MatDialog} from '@angular/material/dialog';
import {ReportDialogComponent} from '../../../dialog/report-dialog/report-dialog.component';
import {ReportHttpService} from '../../../services/http/report-http.service';
import {Report} from '../../../models/report.model';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.scss']
})
export class EmployeeDetailsComponent implements OnInit {
  employee: Worker | undefined;
  manager: Worker | undefined;
  tasks: Task[] = [];
  subordinates: Worker[] = [];

  constructor(private route: ActivatedRoute,
              private employeeHttpService: WorkerHttpService,
              private taskHttpService: TaskHttpService,
              private reportHttpService: ReportHttpService,
              private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.loadData(id).then();
      }
    });
  }

  async loadData(id: number) {
    try {
      // Start the first promise and wait for it to complete
      this.employee = await this.employeeHttpService.getWorkerById(id);

      // Start the second promise after the first one has completed
      this.manager = await this.employeeHttpService.getManagerById(this.employee.managerId);

      this.tasks = await this.taskHttpService.GetAllTasksForWorker(id);

      if (this.employee?.employeesIds.length > 0) {
        this.subordinates = await this.employeeHttpService.getManagerWorkers(id);
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  openReportDialog() {
    const dialogRef = this.dialog.open(ReportDialogComponent);

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result?.length > 0) {
        const report: Report = {
          id: 0,
          text: result,
          assignedDate: new Date(),
          assigneeId: this.manager!.id
        };
        this.reportHttpService.createReport(report, this.manager!.id).then((res) => {

        });
      }
    });
  }
}
