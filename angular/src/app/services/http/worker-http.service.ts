import {Injectable} from '@angular/core';
import {BaseHttpService} from './base-http.service';
import {Worker} from '../../models/worker.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WorkerHttpService extends BaseHttpService {
  protected baseUrl = `${environment.apiUrl}/worker/`;

  getAllWorkers(): Promise<Worker[]> {
    return this.sendRequest<Worker[]>('GET', this.baseUrl);
  }

  getWorkerById(id: number): Promise<Worker> {
    return this.sendRequest<Worker>('GET', `${this.baseUrl}${id}`);
  }

  getAllEmployees(): Promise<Worker[]> {
    return this.sendRequest<Worker[]>('GET', `${this.baseUrl}employees`);
  }

  getEmployeeById(id: number): Promise<Worker> {
    return this.sendRequest<Worker>('GET', `${this.baseUrl}employee/${id}`);
  }

  getAllManagers(): Promise<Worker[]> {
    return this.sendRequest<Worker[]>('GET', `${this.baseUrl}managers`);
  }

  getManagerById(id: number): Promise<Worker> {
    return this.sendRequest<Worker>('GET', `${this.baseUrl}manager/${id}`);
  }

  getManagerWorkers(id: number): Promise<Worker[]> {
    return this.sendRequest<Worker[]>('GET', `${this.baseUrl}manager/${id}/workers`);
  }
}
