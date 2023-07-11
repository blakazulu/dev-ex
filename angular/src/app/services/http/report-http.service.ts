import {Injectable} from '@angular/core';
import {Report} from '../../models/report.model';
import {BaseHttpService} from './base-http.service';
import {environment} from '../../../environments/environment';
import {HttpHeaders, HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReportHttpService extends BaseHttpService {
  protected baseUrl = `${environment.apiUrl}/report/`;

  getAllReports(): Promise<Report[]> {
    return this.sendRequest<Report[]>('GET', this.baseUrl);
  }

  getReportById(id: number): Promise<Report> {
    return this.sendRequest<Report>('GET', `${this.baseUrl}${id}`);
  }

  createReport(report: Report, managerId: number): Promise<Report> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const params = new HttpParams().set('managerId', managerId.toString());

    return this.sendRequest<Report>('POST', this.baseUrl, report, headers, params);
  }

  getReportsForWorker(workerId: number): Promise<Report[]> {
    return this.sendRequest<Report[]>('GET', `${this.baseUrl}worker/${workerId}`);
  }

  getReportsForManager(managerId: number): Promise<Report[]> {
    return this.sendRequest<Report[]>('GET', `${this.baseUrl}manager/${managerId}`);
  }
}
